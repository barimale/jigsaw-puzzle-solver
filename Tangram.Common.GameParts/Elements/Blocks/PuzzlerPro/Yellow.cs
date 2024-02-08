using NetTopologySuite.Geometries;
using System.Drawing;
using Tangram.GameParts.Elements.Elements.Blocks.CommonSettings;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Tangram.GameParts.Elements.Elements.Blocks.PuzzlerPro
{
    public sealed class Yellow : PuzzleProBaseBlock
    {
        public Yellow()
        {
            color = Color.Yellow;

            polygon = new GeometryFactory()
                    .CreatePolygon(new Coordinate[] {
                        new Coordinate(1.00,1.00),
                        new Coordinate(1.00,0.00),
                        new Coordinate(2.00,0.00),
                        new Coordinate(2.00,4.00),
                        new Coordinate(1.00,4.00),
                        new Coordinate(1.00,2.00),
                        new Coordinate(0.00,2.00),
                        new Coordinate(0.00,1.00),
                        new Coordinate(1.00,1.00)
                    });
        }

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            var bloczekDoNarysowania = new Yellow()
                .CreateNew().ToString();

            return new Yellow()
                .CreateNew();
        }
    }
}
