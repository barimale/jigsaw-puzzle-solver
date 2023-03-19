using Genetic.Algorithm.Tangram.GameParts.Elements.Blocks.CommonSettings;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using NetTopologySuite.Geometries;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.GameParts.Elements.Blocks
{
    public sealed class Purple : PolishGameBaseBlock
    {
        public Purple()
        {
            fieldRestriction1side = new object[,] {
                { "O", NA, "O" },
                { "X", "O", "X" }
            };

            fieldRestriction2side = new object[,] {
                { "X", NA, "X" },
                { "O", "X", "O" }
            };

            color = Color.Purple;

            polygon = new GeometryFactory()
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
        }

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            var bloczekDoNarysowania = new Purple()
                .CreateNew(withFieldRestrictions).ToString();

            return new Purple()
                .CreateNew(withFieldRestrictions);
        }
    }
}
