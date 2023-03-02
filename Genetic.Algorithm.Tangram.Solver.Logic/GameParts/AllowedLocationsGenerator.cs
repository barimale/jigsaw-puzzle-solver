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
            foreach (var block in blocks)
            {
                var locations = Generate(
                    block,
                    board,
                    angles);

                block.SetAllowedLocations(locations);

                modified.Add(block);
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

            for(int i = minX; i <= maxX; i++)
            {
                for(int j = minY; j <= maxY; j++)
                {
                    for(int a = 0; a < anglesCount; a++)
                    {
                        var modified = block.Clone();
                        modified.Reflection();
                        modified.Rotate(allowedAngles[a]);
                        modified.MoveTo(i, j);
                        if (board.Polygon.Covers(modified.Polygon))
                        {
                            locations.Add(modified.Polygon);
                        }

                        var modifiedWithoutFlip = block.Clone();
                        modifiedWithoutFlip.Rotate(allowedAngles[a]);
                        modifiedWithoutFlip.MoveTo(i, j);
                        if (board.Polygon.Covers(modifiedWithoutFlip.Polygon))
                        {
                            locations.Add(modifiedWithoutFlip.Polygon);
                        }
                    }
                }
            }

            // TODO: check distinct
            return locations
                .Distinct()
                .ToArray();
        }
    }
}
