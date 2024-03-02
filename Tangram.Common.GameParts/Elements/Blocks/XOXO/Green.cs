using NetTopologySuite.Geometries;
using System.Drawing;
using Tangram.GameParts.Elements.Elements.Blocks.CommonSettings;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Tangram.GameParts.Elements.Elements.Blocks.XOXO
{
    public sealed class Green : PolishGameBaseBlock
    {
        public Green()
        {
            fieldRestriction1side = new object[,] {
                { "O", "X", NA},
                { NA, "O", NA},
                { NA, "X", "O"}
            };

            fieldRestriction2side = new object[,] {
                { NA, "O", "X" },
                { NA, "X", NA },
                { "X", "O", NA }
            };

            color = Color.Green;

            polygon = new GeometryFactory()
                    .CreatePolygon(new Coordinate[] {
                        new Coordinate(0,0),// first the same as last
                        new Coordinate(0,1),
                        new Coordinate(1,1),
                        new Coordinate(1,3),
                        new Coordinate(3,3),
                        new Coordinate(3,2),
                        new Coordinate(2,2),
                        new Coordinate(2,0),
                        new Coordinate(0,0)// last the same as first
                    });
        }

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            var bloczekDoNarysowania = new Green()
                .CreateNew(withFieldRestrictions).ToString();

            return new Green()
                .CreateNew(withFieldRestrictions);
        }
    }
}
