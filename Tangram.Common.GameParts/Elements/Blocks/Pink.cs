using NetTopologySuite.Geometries;
using System.Drawing;
using Tangram.GameParts.Elements.Elements.Blocks.CommonSettings;
using Tangram.GameParts.Logic.Block;

namespace Tangram.GameParts.Elements.Elements.Blocks
{
    public sealed class Pink : PolishGameBaseBlock
    {
        public Pink()
        {
            fieldRestriction1side = new object[,] {
                { NA, "X", "O", "X" },
                { "X", "O", NA, NA }
            };

            fieldRestriction2side = new object[,] {
                { "O", "X", "O", NA },
                {  NA, NA, "X", "O" }
            };

            color = Color.Pink;

            polygon = new GeometryFactory()
                    .CreatePolygon(new Coordinate[] {
                        new Coordinate(0,0),// first the same as last
                        new Coordinate(0,1),
                        new Coordinate(1,1),
                        new Coordinate(1,2),
                        new Coordinate(4,2),
                        new Coordinate(4,1),
                        new Coordinate(2,1),
                        new Coordinate(2,0),
                        new Coordinate(0,0)// last the same as first
                    });
        }

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            var bloczekDoNarysowania = new Pink()
                .CreateNew(withFieldRestrictions).ToString();

            return new Pink()
                .CreateNew(withFieldRestrictions);
        }
    }
}
