using Genetic.Algorithm.Tangram.GameParts.Blocks.CommonSettings;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using NetTopologySuite.Geometries;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.GameParts.Blocks
{
    public sealed class Black: PolishGameBaseBlock
    {
        public Black()
        {
            base.fieldRestriction1side = new object[,] {
                { "O" }
            };

            base.fieldRestriction2side = new object[,] {
                { "X"}
            };

            base.color = Color.Blue;

            base.polygon = new GeometryFactory()
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
