using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using NetTopologySuite.Geometries;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.GameParts.Blocks
{
    public static class Red
    {
        public static BlockBase Create()
        {
            var color = Color.Red;

            var polygon = new GeometryFactory()
                .CreatePolygon(new Coordinate[] {
                    new Coordinate(0,0),// first the same as last
                    new Coordinate(1,0),
                    new Coordinate(1,2),
                    new Coordinate(2,2),
                    new Coordinate(2,3),
                    new Coordinate(1,3),
                    new Coordinate(1,4),
                    new Coordinate(0,4),
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
