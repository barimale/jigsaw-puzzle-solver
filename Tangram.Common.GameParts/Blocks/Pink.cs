using Genetic.Algorithm.Tangram.GameParts.Blocks.CommonSettings;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using NetTopologySuite.Geometries;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.GameParts.Blocks
{
    public sealed class Pink : PolishGameBaseBlock
    {
        public Pink()
        {
            base.fieldRestriction1side = new object[,] {
                { NA, "X", "O", "X" },
                { "X", "O", NA, NA }
            };

            base.fieldRestriction2side = new object[,] {
                { "O", "X", "O", NA },
                {  NA, NA, "X", "O" }
            };

            base.color = Color.Pink;

            base.polygon = new GeometryFactory()
                    .CreatePolygon(new Coordinate[] {
                        new Coordinate(0,0),// first the same as last
                        new Coordinate(0,1),
                        new Coordinate(1,1),
                        new Coordinate(1,2),
                        new Coordinate(4,2),
                        new Coordinate(4,1),
                        new Coordinate(2,1),
                        new Coordinate(2,0),
                        new Coordinate(0,0)// last the same as first
                    });
        }

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            var bloczekDoNarysowania = new Pink()
                .CreateNew(withFieldRestrictions).ToString();

            return new Pink()
                .CreateNew(withFieldRestrictions);
        }
    }
}
