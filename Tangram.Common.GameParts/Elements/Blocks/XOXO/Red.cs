using NetTopologySuite.Geometries;
using System.Drawing;
using Tangram.GameParts.Elements.Elements.Blocks.CommonSettings;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Tangram.GameParts.Elements.Elements.Blocks.XOXO
{
    public sealed class Red : PolishGameBaseBlock
    {
        public Red()
        {
            fieldRestriction1side = new object[,] {
                { "O", NA },
                { "X", NA },
                { "O", "X" },
                { "X", NA }
            };

            fieldRestriction2side = new object[,] {
                { NA, "X" },
                { NA, "O" },
                { "O", "X" },
                { NA, "O" }
            };

            color = Color.Red;

            polygon = new GeometryFactory()
                    .CreatePolygon(new Coordinate[] {
                        new Coordinate(0,0),// first the same as last
                        new Coordinate(1,0),
                        new Coordinate(1,2),
                        new Coordinate(2,2),
                        new Coordinate(2,3),
                        new Coordinate(1,3),
                        new Coordinate(1,4),
                        new Coordinate(0,4),
                        new Coordinate(0,0)// last the same as first
                    });
        }

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            var bloczekDoNarysowania = new Red()
                .CreateNew(withFieldRestrictions).ToString();

            return new Red()
                .CreateNew(withFieldRestrictions);
        }
    }
}
