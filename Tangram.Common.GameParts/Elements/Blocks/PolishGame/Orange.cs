using NetTopologySuite.Geometries;
using System.Drawing;
using Tangram.GameParts.Elements.Elements.Blocks.CommonSettings;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Tangram.GameParts.Elements.Elements.Blocks.PolishGame
{
    public sealed class Orange : PolishGameBaseBlock
    {
        public Orange()
        {
            fieldRestriction1side = new object[,] {
                { NA, "X", "O" },
                { "X", "O", NA },
                { "O", NA, NA }
            };

            fieldRestriction2side = new object[,] {
                { "X", "O", NA },
                { NA, "X", "O" },
                { NA, NA, "X" }
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
