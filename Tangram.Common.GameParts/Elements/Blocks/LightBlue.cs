using NetTopologySuite.Geometries;
using System.Drawing;
using Tangram.GameParts.Elements.Elements.Blocks.CommonSettings;
using Tangram.GameParts.Logic.Block;

namespace Tangram.GameParts.Elements.Elements.Blocks
{
    public sealed class LightBlue : PolishGameBaseBlock
    {
        public LightBlue()
        {
            fieldRestriction1side = new object[,] {
                { "O", NA, NA},
                { "X", NA, NA},
                { "O", "X", "O"}
            };

            fieldRestriction2side = new object[,] {
                { NA, NA, "X" },
                { NA, NA, "O" },
                { "X", "O", "X" }
            };

            color = Color.LightBlue;

            polygon = new GeometryFactory()
                    .CreatePolygon(new Coordinate[] {
                        new Coordinate(0,0),// first the same as last
                        new Coordinate(0,3),
                        new Coordinate(1,3),
                        new Coordinate(1,1),
                        new Coordinate(3,1),
                        new Coordinate(3,0),
                        new Coordinate(0,0)// last the same as first
                    });
        }

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            var bloczekDoNarysowania = new LightBlue()
                .CreateNew(withFieldRestrictions).ToString();

            return new LightBlue()
                .CreateNew(withFieldRestrictions);
        }
    }
}
