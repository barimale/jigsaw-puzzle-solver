using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks;
using NetTopologySuite.Geometries;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.Data.BigBoard.Blocks
{
    public static class Orange
    {
        public static BlockBase Create()
        {
            var color = Color.Orange;

            var polygon = new GeometryFactory()
                .CreatePolygon(new Coordinate[] {
                    new Coordinate(0,1),// first the same as last
                    new Coordinate(0,3),
                    new Coordinate(1,3),
                    new Coordinate(1,2),
                    new Coordinate(2,2),
                    new Coordinate(2,1),
                    new Coordinate(3,1),
                    new Coordinate(3,0),
                    new Coordinate(1,0),
                    new Coordinate(1,1),
                    new Coordinate(0,1)// last the same as first
                });

            var bloczek = new BlockBase(
                polygon,
                color);

            var punktyBloczkaDoNarysowania = bloczek
                .ToString();

            return bloczek;
        }
    }
}
