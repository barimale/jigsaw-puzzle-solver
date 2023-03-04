using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board;
using NetTopologySuite.Geometries;

namespace Genetic.Algorithm.Tangram.Solver.Logic.GameParts
{
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

            var hasToBeFlipped = true;
            for (int a = 0; a < anglesCount; a++)
            {
                for (int i = minX; i <= maxX; i++)
                {
                    for (int j = minY; j <= maxY; j++)
                    {
                        var newWithFlip = Check(block, board, allowedAngles, i, j, a, hasToBeFlipped);

                        if (newWithFlip != null)
                            locations.Add(newWithFlip);
                    }
                }
            }

            hasToBeFlipped = false;
            for (int a = 0; a < anglesCount; a++)
            {
                for (int i = minX; i <= maxX; i++)
                {
                    for (int j = minY; j <= maxY; j++)
                    {
                        var newWithFlip = Check(block, board, allowedAngles, i, j, a, hasToBeFlipped);

                        if (newWithFlip != null)
                            locations.Add(newWithFlip);
                    }
                }
            }

            var distinctedLocations = new HashSet<Geometry>();
            foreach(var location in locations)
            {
                var exists = distinctedLocations.Any(p =>
                {
                    var zip1 =  p
                        .Coordinates
                        .ToList()
                        .DistinctBy(d => $@"{d.X} - {d.Y}")
                        .OrderBy(pp => pp.X)
                        .ThenBy(ppp => ppp.Y)
                        .ToList();

                    var zip2 =
                            location
                                .Coordinates
                                .ToList()
                                .DistinctBy(dd => $@"{dd.X} - {dd.Y}")
                                .OrderBy(ss => ss.X)
                                .ThenBy(sss => sss.Y)
                                .ToList();

                    var zipped = zip1.Zip(zip2);

                    var result = true;
                    foreach(var pair in zipped)
                    {
                        if(pair.First.X != pair.Second.X || pair.First.Y != pair.Second.Y)
                        {
                            result = false;
                        }
                    }

                    return result;
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
            if(hasToBeFlipped)
            {
                modified.Reflection();
            }
            modified.Rotate(allowedAngles[a]);
            modified.MoveTo(i, j);

            //var slightlyBiggerBoard = board.CoverableBoard;
            if (board.Polygon.Covers(modified.Polygon))
            {
                var newGeometry = new GeometryFactory()
                    .CreateGeometry(modified.Polygon);

                var digits = 3;
                var minimalDiff = 0.001d;

                foreach (var cc in newGeometry.Coordinates)
                {
                    if (Math.Abs(cc.CoordinateValue.X - Math.Round(cc.CoordinateValue.X, digits, MidpointRounding.ToEven)) < minimalDiff)
                    {
                        var result = Convert.ToInt32(
                                Math.Round(cc.CoordinateValue.X, digits, MidpointRounding.ToEven));
                        cc.CoordinateValue.X = result;
                    }

                    if (Math.Abs(cc.CoordinateValue.Y - Math.Round(cc.CoordinateValue.Y, digits, MidpointRounding.ToEven)) < minimalDiff)
                    {
                        var result = Convert.ToInt32(
                                Math.Round(cc.CoordinateValue.Y, digits, MidpointRounding.ToEven));
                        cc.CoordinateValue.Y = result;
                    }
                }

                return newGeometry;
            }

            return null; // null here?
        }
    }
}
