using NetTopologySuite.Geometries;
using System.Drawing;
using Tangram.GameParts.Elements.Elements.Blocks.CommonSettings;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Tangram.GameParts.Elements.Elements.Blocks.PolishGame
{
    public sealed class Black : PolishGameBaseBlock
    {
        public Black()
        {
            fieldRestriction1side = new object[,] {
                { "O" }
            };

            fieldRestriction2side = new object[,] {
                { "X"}
            };

            color = Color.Black;

            polygon = new GeometryFactory()
                    .CreatePolygon(new Coordinate[] {
                        new Coordinate(0,0),// first the same as last
                        new Coordinate(1,0),
                        new Coordinate(1,1),
                        new Coordinate(0,1),
                        new Coordinate(0,0)// last the same as first
                    });
        }

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            var bloczekDoNarysowania = new Black()
                .CreateNew(withFieldRestrictions).ToString();

            return new Black()
                .CreateNew(withFieldRestrictions);
        }
    }
}
