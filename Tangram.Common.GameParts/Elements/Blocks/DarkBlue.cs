using NetTopologySuite.Geometries;
using System.Drawing;
using Tangram.GameParts.Elements.Elements.Blocks.CommonSettings;
using Tangram.GameParts.Logic.Block;

namespace Tangram.GameParts.Elements.Elements.Blocks
{
    public sealed class DarkBlue : PolishGameBaseBlock
    {
        public DarkBlue()
        {
            fieldRestriction1side = new object[,] {
                { "O", "X", "O", "X", "O" }
            };

            fieldRestriction2side = new object[,] {
                { "X", "O", "X", "O", "X" }
            };

            color = Color.DarkBlue;

            polygon = new GeometryFactory()
                    .CreatePolygon(new Coordinate[] {
                        new Coordinate(0,0),// first the same as last
                        new Coordinate(0,1),
                        new Coordinate(5,1),
                        new Coordinate(5,0),
                        new Coordinate(0,0)// last the same as first
                    });
        }

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            var bloczekDoNarysowania = new DarkBlue()
                .CreateNew(withFieldRestrictions).ToString();

            return new DarkBlue()
                .CreateNew(withFieldRestrictions);
        }
    }
}
