using Genetic.Algorithm.Tangram.GameParts.Blocks.CommonSettings;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using NetTopologySuite.Geometries;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.GameParts.Blocks
{
    public sealed class LightGreen : PolishGameBaseBlock
    {
        public LightGreen()
        {
            base.fieldRestriction1side = new object[,] {
                { "O", NA, NA, NA},
                { "X", "O", "X", "O" }
            };

            base.fieldRestriction2side = new object[,] {
                { NA, NA, NA, "X" },
                { "X", "O", "X", "O" }
            };

            base.color = Color.LightGreen;

            base.polygon = new GeometryFactory()
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
