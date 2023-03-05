using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using NetTopologySuite.Geometries;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.GameParts.Blocks
{
    public static class Pink
    {
        public static BlockBase Create()
        {
            var color = Color.Pink;

            var polygon = new GeometryFactory()
                .CreatePolygon(new Coordinate[] {
                    new Coordinate(0,0),// first the same as last
                    new Coordinate(0,1),
                    new Coordinate(1,1),
                    new Coordinate(1,2),
                    new Coordinate(4,2),
                    new Coordinate(4,1),
                    new Coordinate(2,1),
                    new Coordinate(2,0),
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
