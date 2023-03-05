using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using NetTopologySuite.Geometries;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.GameParts.Blocks
{
    public static class Purple
    {
        public static BlockBase Create()
        {
            var color = Color.Purple;

            var polygon = new GeometryFactory()
                .CreatePolygon(new Coordinate[] {
                    new Coordinate(0,0),// first the same as last
                    new Coordinate(0,2),
                    new Coordinate(1,2),
                    new Coordinate(1,1),
                    new Coordinate(2,1),
                    new Coordinate(2,2),
                    new Coordinate(3,2),
                    new Coordinate(3,0),
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
