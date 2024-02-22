using NetTopologySuite.Geometries;
using System.Drawing;
using Tangram.GameParts.Elements.Elements.Blocks.CommonSettings;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Tangram.GameParts.Elements.Elements.Blocks.PuzzlerPro
{
    public sealed class Blue: PuzzleProBaseBlock
    {
        public Blue()
        {
            fieldRestriction1side = new object[,] {
                { "X", "X","X" },
                { "X", NA,NA },
                { "X", NA, NA }
            };

            fieldRestriction2side = new object[,] {
                { "X", "X","X" },
                { NA, NA,"X" },
                { NA, NA,"X" }
            };

            color = Color.Blue;

            polygon = new GeometryFactory()
                    .CreatePolygon(new Coordinate[] {
                        new Coordinate(0.00,3.00) , // first the same as last
                        new Coordinate(0.00,0.00) ,
                        new Coordinate(3.00,0.00),
                        new Coordinate(3.00,1.00) ,
                        new Coordinate(1.00,1.00) ,
                        new Coordinate(1.00,3.00) ,
                        new Coordinate(0.00,3.00)// last the same as first
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
