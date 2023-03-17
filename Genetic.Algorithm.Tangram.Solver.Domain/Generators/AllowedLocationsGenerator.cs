using Genetic.Algorithm.Tangram.Common.Extensions.Extensions;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Prepared;

namespace Genetic.Algorithm.Tangram.Solver.Domain.Generators;

public class AllowedLocationsGenerator
{
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
                angles);

            block.SetAllowedLocations(locations);

            modified.Add(block.Clone());
        }

        return modified;
    }
    private Geometry[] Generate(BlockBase block, BoardShapeBase board, int[] allowedAngles)
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

            var newWithFlip = Check(
                block,
                board,
                allowedAngles,
                (int)permutationAsArray[0],
                (int)permutationAsArray[1],
                (int)permutationAsArray[2],
                (bool)permutationAsArray[3]
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

    private Geometry? Check(
        BlockBase block,
        BoardShapeBase board,
        int[] allowedAngles,
        int i,
        int j,
        int a,
        bool hasToBeFlipped)
    {
        BlockBase modified = block.Clone(true);
        if (hasToBeFlipped)
        {
            modified.Reflection();
        }
        modified.Rotate(allowedAngles[a]);
        modified.MoveTo(i, j);

        IPreparedGeometry preparedBoard = PreparedGeometryFactory
            .Prepare(board.Polygon);

        if (preparedBoard.Covers(modified.Polygon))
        {
            var newGeometry = new GeometryFactory()
                .CreateGeometry(modified.Polygon);

            var digits = 3;
            var minimalDiff = 0.001d;

            foreach (var cc in newGeometry.Coordinates)
            {
                if (Math.Abs(cc.CoordinateValue.X - Math.Round(cc.CoordinateValue.X, digits, MidpointRounding.AwayFromZero)) < minimalDiff)
                {
                    var result = Convert.ToInt32(
                            Math.Round(cc.CoordinateValue.X, digits, MidpointRounding.AwayFromZero));
                    cc.CoordinateValue.X = result;
                }

                if (Math.Abs(cc.CoordinateValue.Y - Math.Round(cc.CoordinateValue.Y, digits, MidpointRounding.AwayFromZero)) < minimalDiff)
                {
                    var result = Convert.ToInt32(
                            Math.Round(cc.CoordinateValue.Y, digits, MidpointRounding.AwayFromZero));
                    cc.CoordinateValue.Y = result;
                }
            }

            // execute the extraristrictedcheck

            return newGeometry;
        }

        return null;
    }
}