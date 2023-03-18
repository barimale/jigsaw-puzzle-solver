using Genetic.Algorithm.Tangram.GameParts.Blocks.CommonSettings;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using NetTopologySuite.Geometries;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.GameParts.Blocks
{
    public sealed class Red : PolishGameBaseBlock
    {
        public Red()
        {
            base.fieldRestriction1side = new object[,] {
                { "X", NA },
                { "O", "X" },
                { "X", NA },
                { "O", NA }
            };

            base.fieldRestriction2side = new object[,] {
                { NA, "O" },
                { "O", "X" },
                { NA, "O" },
                { NA, "X" }
            };

            base.color = Color.Red;

            base.polygon = new GeometryFactory()
                    .CreatePolygon(new Coordinate[] {
                        new Coordinate(0,0),// first the same as last
                        new Coordinate(1,0),
                        new Coordinate(1,2),
                        new Coordinate(2,2),
                        new Coordinate(2,3),
                        new Coordinate(1,3),
                        new Coordinate(1,4),
                        new Coordinate(0,4),
                        new Coordinate(0,0)// last the same as first
                    });
        }

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            var bloczekDoNarysowania = new Red()
                .CreateNew(withFieldRestrictions).ToString();

            return new Red()
                .CreateNew(withFieldRestrictions);
        }
    }
}
