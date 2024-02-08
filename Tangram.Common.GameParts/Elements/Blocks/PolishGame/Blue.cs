using NetTopologySuite.Geometries;
using System.Drawing;
using Tangram.GameParts.Elements.Elements.Blocks.CommonSettings;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Tangram.GameParts.Elements.Elements.Blocks.PolishGame
{
    public sealed class Blue : PolishGameBaseBlock
    {
        public Blue()
        {
            fieldRestriction1side = new object[,] {
                { "O", "X" },
                { "X", "O" },
                { "O", NA }
            };

            fieldRestriction2side = new object[,] {
                { "O", "X" },
                { "X", "O" },
                { NA, "X" }
            };

            color = Color.Blue;

            polygon = new GeometryFactory()
                    .CreatePolygon(new Coordinate[] {
                        new Coordinate(0,0),// first the same as last
                        new Coordinate(0,3),
                        new Coordinate(1,3),
                        new Coordinate(1,2),
                        new Coordinate(2,2),
                        new Coordinate(2,0),
                        new Coordinate(0,0)// last the same as first
                    });
        }

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            var bloczekDoNarysowania = new Blue()
                .CreateNew(withFieldRestrictions).ToString();

            return new Blue()
                .CreateNew(withFieldRestrictions);
        }
    }
}
