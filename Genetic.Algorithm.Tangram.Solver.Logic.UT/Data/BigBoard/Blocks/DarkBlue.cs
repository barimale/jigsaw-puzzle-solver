using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks;
using NetTopologySuite.Geometries;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.Data.BigBoard.Blocks
{
    public static class DarkBlue
    {
        public static BlockBase Create()
        {
            var color = Color.DarkBlue;

            var polygon = new GeometryFactory()
                .CreatePolygon(new Coordinate[] {
                    new Coordinate(0,0),// first the same as last
                    new Coordinate(0,1),
                    new Coordinate(5,1),
                    new Coordinate(5,0),
                    new Coordinate(0,0)// last the same as first
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
