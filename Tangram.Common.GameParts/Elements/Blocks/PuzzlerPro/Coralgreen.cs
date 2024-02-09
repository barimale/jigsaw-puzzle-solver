using NetTopologySuite.Geometries;
using System.Drawing;
using Tangram.GameParts.Elements.Elements.Blocks.CommonSettings;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Tangram.GameParts.Elements.Elements.Blocks.PuzzlerPro
{
    public sealed class Coralgreen : PuzzleProBaseBlock
    {
        public Coralgreen()
        {
            fieldRestriction1side = new object[,] {
                { "X", "X" },
                { "X", "X" },
                { "X", NA }
            };

            fieldRestriction2side = new object[,] {
                { "X", "X" },
                { "X", "X" },
                { NA, "X" }
            };

            color = Color.LimeGreen;

            polygon = new GeometryFactory()
                    .CreatePolygon(new Coordinate[] {
                        new Coordinate(0.00,1.00),
                        new Coordinate(0.00,0.00),
                        new Coordinate(1.00,0.00),
                        new Coordinate(1.00,1.00),
                        new Coordinate(2.00,1.00),
                        new Coordinate(2.00,3.00),
                        new Coordinate(0.00,3.00),
                        new Coordinate(0.00,1.00)
                    });
        }

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            var bloczekDoNarysowania = new Coralgreen()
                .CreateNew(withFieldRestrictions).ToString();

            return new Coralgreen()
                .CreateNew(withFieldRestrictions);
        }
    }
}
