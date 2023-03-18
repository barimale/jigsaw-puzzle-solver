using Genetic.Algorithm.Tangram.GameParts.Blocks.CommonSettings;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using NetTopologySuite.Geometries;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.GameParts.Blocks
{
    public sealed class Green : PolishGameBaseBlock
    {
        public Green()
        {
            base.fieldRestriction1side = new object[,] {
                { NA, "X", "O"},
                { NA, "O", NA},
                { "O", "X", NA}
            };

            base.fieldRestriction2side = new object[,] {
                { "X", "O", NA },
                { NA, "X", NA },
                { NA, "O", "X" }
            };

            base.color = Color.Green;

            base.polygon = new GeometryFactory()
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
