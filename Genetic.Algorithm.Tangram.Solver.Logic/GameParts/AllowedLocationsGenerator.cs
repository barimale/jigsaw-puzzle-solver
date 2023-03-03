﻿using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks;
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
            // there are the same geometricies with shifted coordinates,
            // and as the first needs to be the last
            // not so easy to distinct in a regular way
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

                var digits = 3;
                var minimalDiff = 0.001d;
                foreach(var cc in newGeometry.Coordinates)
                {
                    if(Math.Abs(cc.CoordinateValue.X - Math.Round(cc.CoordinateValue.X,digits,MidpointRounding.ToEven)) < minimalDiff)
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

            return null;
        }
    }
}
