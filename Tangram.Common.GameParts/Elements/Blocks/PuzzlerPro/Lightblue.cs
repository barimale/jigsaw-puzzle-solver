using NetTopologySuite.Geometries;
using System.Drawing;
using Tangram.GameParts.Elements.Elements.Blocks.CommonSettings;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Tangram.GameParts.Elements.Elements.Blocks.PuzzlerPro
{
    public sealed class Lightblue : PuzzleProBaseBlock
    {
        public Lightblue()
        {
            fieldRestriction1side = new object[,] {
                { "X", "X" },
                { "X", NA }
            };

            fieldRestriction2side = new object[,] {
                { "X", "X" },
                { NA, "X" }
            };

            color = Color.AliceBlue;

            polygon = new GeometryFactory()
                    .CreatePolygon(new Coordinate[] {
                        new Coordinate(0.00,0.00),
                        new Coordinate(1.00,0.00),
                        new Coordinate(1.00,1.00),
                        new Coordinate(2.00,1.00),
                        new Coordinate(2.00,2.00),
                        new Coordinate(0.00,2.00),
                        new Coordinate(0.00,0.00)
                    });
        }

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            var bloczekDoNarysowania = new Lightblue()
                .CreateNew(withFieldRestrictions).ToString();

            return new Lightblue()
                .CreateNew(withFieldRestrictions);
        }
    }
}
