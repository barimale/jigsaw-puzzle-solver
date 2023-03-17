using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using NetTopologySuite.Geometries;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.GameParts.Blocks
{
    public sealed class Black
    {
        private static object[,] fieldRestriction1side = new object[,] {
            { "O" }
        };

        private static object[,] fieldRestriction2side = new object[,] {
            { "X"}
        };

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            var color = Color.Black;

            var polygon = new GeometryFactory()
                .CreatePolygon(new Coordinate[] {
                    new Coordinate(0,0),// first the same as last
                    new Coordinate(1,0),
                    new Coordinate(1,1),
                    new Coordinate(0,1),
                    new Coordinate(0,0)// last the same as first
                });

            if (!withFieldRestrictions)
            {
                return new BlockBase(
                polygon,
                color);
            }

            var bloczek = new BlockBase(
                polygon,
                color,
                new[] { fieldRestriction1side, fieldRestriction2side });

            var punktyBloczkaDoNarysowania = bloczek
                .ToString();

            return bloczek;
        }
    }
}
