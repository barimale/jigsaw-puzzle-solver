using NetTopologySuite.Geometries;
using System.Drawing;
using Tangram.GameParts.Elements.Elements.Blocks.CommonSettings;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Tangram.GameParts.Elements.Elements.Blocks.PolishGame
{
    public sealed class LightGreen : PolishGameBaseBlock
    {
        public LightGreen()
        {
            fieldRestriction1side = new object[,] {
                { "X", "O", "X", "O" },
                { "O", NA, NA, NA}
            };

            fieldRestriction2side = new object[,] {
                { "X", "O", "X", "O" },
                { NA, NA, NA, "X" }
            };

            color = Color.LightGreen;

            polygon = new GeometryFactory()
                    .CreatePolygon(new Coordinate[] {
                        new Coordinate(0,0),// first the same as last
                        new Coordinate(0,2),
                        new Coordinate(1,2),
                        new Coordinate(1,1),
                        new Coordinate(4,1),
                        new Coordinate(4,0),
                        new Coordinate(0,0)// last the same as first
                    });
        }

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            var bloczekDoNarysowania = new LightGreen()
                .CreateNew(withFieldRestrictions).ToString();

            return new LightGreen()
                .CreateNew(withFieldRestrictions);
        }
    }
}
