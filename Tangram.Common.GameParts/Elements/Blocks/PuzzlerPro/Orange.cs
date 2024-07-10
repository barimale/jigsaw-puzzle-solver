using NetTopologySuite.Geometries;
using System.Drawing;
using Tangram.GameParts.Elements.Elements.Blocks.CommonSettings;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Tangram.GameParts.Elements.Elements.Blocks.PuzzlerPro
{
    public sealed class Orange: PuzzleProBaseBlock
    {
        public Orange()
        {
            fieldRestriction1side = new object[,] {
                { NA, "X", "X" },
                {"X", "X", NA },
                { NA, "X", NA }
            };

            fieldRestriction2side = new object[,] {
               { "X", "X", NA },
                {NA, "X", "X" },
                 { NA, "X", NA }
            };

            color = Color.Orange;

            polygon = new GeometryFactory()
                    .CreatePolygon(new Coordinate[] {
                        new Coordinate(1.00,1.00),
                        new Coordinate(1.00,0.00),
                        new Coordinate(3.00,0.00),
                        new Coordinate(3.00,1.00),
                        new Coordinate(2.00,1.00),
                        new Coordinate(2.00,3.00),
                        new Coordinate(1.00,3.00),
                        new Coordinate(1.00,2.00),
                        new Coordinate(0.00,2.00),
                        new Coordinate(0.00,1.00),
                        new Coordinate(1.00,1.00)
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
