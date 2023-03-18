using Genetic.Algorithm.Tangram.GameParts.Blocks.CommonSettings;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using NetTopologySuite.Geometries;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.GameParts.Blocks
{
    // TODO: double check it
    public sealed class Purple: PolishGameBaseBlock
    {
        public Purple()
        {
            base.fieldRestriction1side = new object[,] {
                { "O", NA, "O" },
                { "X", "O", "X" }
            };

            base.fieldRestriction2side = new object[,] {
                { "X", NA, "X" },
                { "O", "X", "O" }
            };

            base.color = Color.Purple;

            base.polygon = new GeometryFactory()
            .CreatePolygon(new Coordinate[] {
                    new Coordinate(0,0),// first the same as last
                    new Coordinate(0,2),
                    new Coordinate(1,2),
                    new Coordinate(1,1),
                    new Coordinate(2,1),
                    new Coordinate(2,2),
                    new Coordinate(3,2),
                    new Coordinate(3,0),
                    new Coordinate(0,0)// last the same as first
            });
        }

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            return new Purple()
                .CreateNew(withFieldRestrictions);
        }
    }
}
