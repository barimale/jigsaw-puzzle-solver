using Genetic.Algorithm.Tangram.Common.Extensions;
using Genetic.Algorithm.Tangram.Common.Extensions.Extensions;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using Genetic.Algorithm.Tangram.Solver.Domain.Extensions;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Prepared;

namespace Genetic.Algorithm.Tangram.Solver.Domain.Generators;

public class AllowedLocationsGenerator
{
    private List<Tuple<string, int>>? allowedMatches;
    private List<object>? skippedMarkup;
    private double fieldHeight;
    private double fieldWidth;

    public AllowedLocationsGenerator()
    {
        // intentionally left blank
    }

    public AllowedLocationsGenerator(
        List<Tuple<string, int>> allowedMatches,
        double fieldHeight,
        double fieldWidth,
        List<object>? skippedMarkup = null)
        : this()
    {
        this.allowedMatches = allowedMatches;
        this.fieldHeight = fieldHeight;
        this.fieldWidth = fieldWidth;
        this.skippedMarkup = skippedMarkup;
    }

    private bool WithMarkups => allowedMatches != null && allowedMatches.Count > 0;

    public IList<BlockBase> Preconfigure(
        IList<BlockBase> blocks,
        BoardShapeBase board,
        int[] angles)
    {
        var modified = new List<BlockBase>();
        // calculate allowed locations of blocks
        foreach (var block in blocks.ToList())
        {
            var locations = Generate(
                block,
                board,
                angles,
                this.WithMarkups
            );

            block.SetAllowedLocations(locations);
            modified.Add(block.Clone());
        }

        return modified;
    }

    private Geometry[] Generate(
        BlockBase block,
        BoardShapeBase board,
        int[] allowedAngles,
        bool withMarkups)
    {
        var locations = new List<Geometry>();

        var minX = (int)board.Polygon.EnvelopeInternal.MinX;
        var maxX = (int)board.Polygon.EnvelopeInternal.MaxX;
        var minY = (int)board.Polygon.EnvelopeInternal.MinY;
        var maxY = (int)board.Polygon.EnvelopeInternal.MaxY;
        var anglesCount = allowedAngles.Length;
        bool[] flips = { false, true };

        object[][] allInputsAsArray = new[] {
            Enumerable.Range(minX, maxX - minX).Select(p => (object)p).ToArray(), // x
            Enumerable.Range(minY, maxY - minY).Select(p => (object)p).ToArray(), // y
            Enumerable.Range(0, anglesCount).Select(p => (object)p).ToArray(), // angles
            flips.Select(p => (object)p).ToArray() // flips
        };

        var permutated = allInputsAsArray.Permutate();
        var permutatedCount = permutated.ToList().Count;

        foreach (var permutation in permutated.ToList())
        {
            var permutationAsArray = permutation.ToArray();

            var newWithFlip = TryCheck(
                block,
                board,
                allowedAngles,
                (int)permutationAsArray[0],
                (int)permutationAsArray[1],
                (int)permutationAsArray[2],
                (bool)permutationAsArray[3],
                withMarkups
            );

            if (newWithFlip != null)
                locations.Add(newWithFlip);
        }

        var distinctedLocations = new HashSet<Geometry>();
        foreach (var location in locations)
        {
            var exists = distinctedLocations.Any(p =>
            {
                return p.CoveredBy(location) && !p.Crosses(location);
            });

            if (!exists)
            {
                distinctedLocations.Add(location);
            }
        }

        return distinctedLocations
            .ToArray();
    }

    private Geometry? TryCheck(
        BlockBase block,
        BoardShapeBase board,
        int[] allowedAngles,
        int i,
        int j,
        int a,
        bool hasToBeFlipped,
        bool withMarkups)
    {
        if(withMarkups)
        {
            return TryCheckWithMarkups(block, board, allowedAngles[a], i, j, hasToBeFlipped);
        }
        else
        {
            return TryCheckWithoutMarkups(block, board, allowedAngles[a], i, j, hasToBeFlipped);
        }
    }

    private Geometry? TryCheckWithMarkups(
        BlockBase block,
        BoardShapeBase board,
        int angleInDegreesFromAllowedAngles,
        int i,
        int j,
        bool hasToBeFlipped)
    {
        BlockBase modifiedMadeOfFields = block.Clone(true);

        var blockSideMarkups = modifiedMadeOfFields.FieldRestrictionMarkups[hasToBeFlipped ? 1 : 0];
        var fieldsToBeTransformed = new RectangularBoardFieldsGenerator()
            .GenerateFields(
                board.ScaleFactor,
                this.fieldHeight,
                this.fieldWidth,
                (int)(modifiedMadeOfFields.Polygon.Boundary.EnvelopeInternal.Width / this.fieldWidth),
                (int)(modifiedMadeOfFields.Polygon.Boundary.EnvelopeInternal.Height / this.fieldHeight),
                blockSideMarkups
            ).ConvertToGeometryCollection();

        if (hasToBeFlipped)
        {
            fieldsToBeTransformed.Reflection();
        }

        fieldsToBeTransformed.Rotate(angleInDegreesFromAllowedAngles);
        fieldsToBeTransformed.MoveTo(i, j);

        // and board fields
        var boardFields = board
            .BoardFieldsDefinition
            .ConvertToGeometryCollection();

        // match fields
        var isOk = fieldsToBeTransformed?
            .ToList()
            .TrueForAll(blockField =>
            {
                var foundBoardField = boardFields?
                    .FirstOrDefault(pp => pp.CoveredBy(blockField));

                if (foundBoardField == null)
                {
                    return false;
                }

                if (this.skippedMarkup != null
                && this.skippedMarkup.Contains(blockField.UserData))
                {
                    return true;
                }

                var matchCandidate = Tuple.Create<string, int>(
                    (string)blockField.UserData,
                    (int)foundBoardField.UserData);

                if (this.allowedMatches != null
                && this.allowedMatches.Contains(matchCandidate))
                {
                    return true;
                }
                else
                { 
                    return false;
                }
            });

        if (!isOk.HasValue || (isOk.HasValue && isOk.Value == false))
        {
            return null;
        }
        else
        {
            return TryCheckWithoutMarkups(
                    block,
                    board,
                    angleInDegreesFromAllowedAngles,
                    i,
                    j,
                    hasToBeFlipped
                );
        }
    }

    private Geometry? TryCheckWithoutMarkups(
        BlockBase block,
        BoardShapeBase board,
        int angleInDegreesFromAllowedAngles,
        int i,
        int j,
        bool hasToBeFlipped)
    {
        BlockBase modified = block.Clone(true);
        if (hasToBeFlipped)
        {
            modified.Reflection();
        }
        modified.Rotate(angleInDegreesFromAllowedAngles);
        modified.MoveTo(i, j);

        IPreparedGeometry preparedBoard = PreparedGeometryFactory
            .Prepare(board.Polygon);

        if (preparedBoard.Covers(modified.Polygon))
        {
            var newGeometry = new GeometryFactory()
                .CreateGeometry(modified.Polygon)
                .WithPolishedCoordinates();

            return newGeometry;
        }

        return null;
    }
}