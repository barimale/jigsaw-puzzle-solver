using NetTopologySuite.Geometries;
using System.Drawing;
using Tangram.GameParts.Elements.Elements.Blocks.CommonSettings;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Tangram.GameParts.Elements.Elements.Blocks.PuzzlerPro
{
    public sealed class Darkred : PuzzleProBaseBlock
    {
        public Darkred()
        {
            fieldRestriction1side = new object[,] {
                { NA, "X" },
                { "X", "X" },
                { "X", NA }
            };

            fieldRestriction2side = new object[,] {
                { "X" , NA},
                { "X", "X" },
                { NA, "X"}
            };

            color = Color.DarkRed;

            polygon = new GeometryFactory()
                    .CreatePolygon(new Coordinate[] {
                        new Coordinate(1.00,0.00),
                        new Coordinate(1.00,1.00),
                        new Coordinate(2.00,1.00),
                        new Coordinate(2.00,3.00),
                        new Coordinate(1.00,3.00),
                        new Coordinate(1.00,2.00),
                        new Coordinate(0.00,2.00),
                        new Coordinate(0.00,0.00),
                        new Coordinate(1.00,0.00)
                    });
        }

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            var bloczekDoNarysowania = new Darkred()
                .CreateNew(withFieldRestrictions).ToString();

            return new Darkred()
                .CreateNew(withFieldRestrictions);
        }
    }
}
