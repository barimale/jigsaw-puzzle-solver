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
                        var newWithFlip = Check(block, board, allowedAngles, i, j, a, true);

                        if(newWithFlip != null)
                            locations.Add(newWithFlip);

                        var newWithoutFlip = Check(block, board, allowedAngles, i, j, a, false);
                        if (newWithoutFlip != null)
                            locations.Add(newWithoutFlip);
                    }
                }
            }

            // TODO: check distinct
            return locations
                .Distinct()
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
            var modified = block.Clone();
            if(hasToBeFlipped)
            {
                modified.Reflection();
            }
            modified.Rotate(allowedAngles[a]);
            modified.MoveTo(i, j);

            if (board.Polygon.Covers(modified.Polygon))
            {
                //modified.Polygon.Normalize();

                var newGeometry = new GeometryFactory()
                    .CreateGeometry(modified.Polygon);

                var minimalDiff = 0.01d; // or d
                foreach(var cc in newGeometry.Coordinates)
                {
                    if(Math.Abs(cc.CoordinateValue.X - (int)cc.CoordinateValue.X) < minimalDiff)
                        cc.CoordinateValue.X = (int)cc.CoordinateValue.X;

                    if (Math.Abs(cc.CoordinateValue.Y - (int)cc.CoordinateValue.Y) < minimalDiff)
                        cc.CoordinateValue.Y = (int)cc.CoordinateValue.Y;
                }

                return newGeometry;
            }

            return null;
        }
    }
}
