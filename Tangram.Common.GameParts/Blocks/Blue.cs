using Genetic.Algorithm.Tangram.GameParts.Blocks.CommonSettings;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using NetTopologySuite.Geometries;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.GameParts.Blocks
{
    public sealed class Blue: PolishGameBaseBlock
    {
        public Blue()
        {
            base.fieldRestriction1side = new object[,] {
                { "O", NA },
                { "X", "O" },
                { "O", "X" }
            };

            base.fieldRestriction2side = new object[,] {
                { NA, "X" },
                { "X", "O" },
                { "O", "X" }
            };

            base.color = Color.Blue;

            base.polygon = new GeometryFactory()
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

            return  new Blue()
                .CreateNew(withFieldRestrictions);
        }
    }
}
