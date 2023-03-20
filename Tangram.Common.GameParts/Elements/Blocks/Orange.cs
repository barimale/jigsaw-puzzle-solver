using Generic.Algorithm.Tangram.GameParts.Elements.Blocks.CommonSettings;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using NetTopologySuite.Geometries;
using System.Drawing;

namespace Generic.Algorithm.Tangram.GameParts.Elements.Blocks
{
    public sealed class Orange : PolishGameBaseBlock
    {
        public Orange()
        {
            fieldRestriction1side = new object[,] {
                { "O", NA, NA },
                { "X", "O", NA },
                { NA, "X", "O" }
            };

            fieldRestriction2side = new object[,] {
                { NA, NA, "X" },
                { NA, "X", "O" },
                { "X", "O", NA }
            };

            color = Color.Orange;

            polygon = new GeometryFactory()
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
        }

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            var bloczekDoNarysowania = new Orange()
                .CreateNew(withFieldRestrictions).ToString();

            return new Orange()
                .CreateNew(withFieldRestrictions);
        }
    }
}
