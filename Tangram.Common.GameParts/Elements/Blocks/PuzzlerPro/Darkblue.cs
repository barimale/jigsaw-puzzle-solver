using NetTopologySuite.Geometries;
using System.Drawing;
using Tangram.GameParts.Elements.Elements.Blocks.CommonSettings;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Tangram.GameParts.Elements.Elements.Blocks.PuzzlerPro
{
    public sealed class Darkblue: PuzzleProBaseBlock
    {
        public Darkblue()
        {
            fieldRestriction1side = new object[,] {
                { "X", NA },
                { "X", NA },
                { "X", "X"}
            };

            fieldRestriction2side = new object[,] {
                { NA, "X" },
                {  NA, "X" },
                {  "X", "X"}
            };

            color = Color.DarkBlue;

            polygon = new GeometryFactory()
                    .CreatePolygon(new Coordinate[] {
                        new Coordinate(0.00,1.00),
                        new Coordinate(0.00,0.00),
                        new Coordinate(1.00,0.00),
                        new Coordinate(1.00,1.00),
                        new Coordinate(1.00,2.00),
                        new Coordinate(2.00,2.00),
                        new Coordinate(2.00,3.00),
                        new Coordinate(0.00,3.00),
                        new Coordinate(0.00,2.00),
                        new Coordinate(0.00,1.00)
                    });
        }

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            var bloczekDoNarysowania = new Darkblue()
                .CreateNew(withFieldRestrictions).ToString();

            return new Darkblue()
                .CreateNew(withFieldRestrictions);
        }
    }
}
