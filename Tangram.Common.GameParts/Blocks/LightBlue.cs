using Genetic.Algorithm.Tangram.GameParts.Blocks.CommonSettings;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using NetTopologySuite.Geometries;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.GameParts.Blocks
{
    public sealed class LightBlue : PolishGameBaseBlock
    {
        public LightBlue()
        {
            base.fieldRestriction1side = new object[,] {
                { "O", NA, NA},
                { "X", NA, NA},
                { "O", "X", "O"}
            };

            base.fieldRestriction2side = new object[,] {
                { NA, NA, "X" },
                { NA, NA, "O" },
                { "X", "O", "X" }
            };

            base.color = Color.LightBlue;

            base.polygon = new GeometryFactory()
                    .CreatePolygon(new Coordinate[] {
                        new Coordinate(0,0),// first the same as last
                        new Coordinate(0,3),
                        new Coordinate(1,3),
                        new Coordinate(1,1),
                        new Coordinate(3,1),
                        new Coordinate(3,0),
                        new Coordinate(0,0)// last the same as first
                    });
        }

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            var bloczekDoNarysowania = new LightBlue()
                .CreateNew(withFieldRestrictions).ToString();

            return new LightBlue()
                .CreateNew(withFieldRestrictions);
        }
    }
}
