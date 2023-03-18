using Genetic.Algorithm.Tangram.GameParts.Blocks.CommonSettings;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using NetTopologySuite.Geometries;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.GameParts.Blocks
{
    public sealed class DarkBlue : PolishGameBaseBlock
    {
        public DarkBlue()
        {
            base.fieldRestriction1side = new object[,] {
                { "O", "X", "O", "X", "O" }
            };

            base.fieldRestriction2side = new object[,] {
                { "X", "O", "X", "O", "X" }
            };

            base.color = Color.DarkBlue;

            base.polygon = new GeometryFactory()
                    .CreatePolygon(new Coordinate[] {
                        new Coordinate(0,0),// first the same as last
                        new Coordinate(0,1),
                        new Coordinate(5,1),
                        new Coordinate(5,0),
                        new Coordinate(0,0)// last the same as first
                    });
        }

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            var bloczekDoNarysowania = new DarkBlue()
                .CreateNew(withFieldRestrictions).ToString();

            return new DarkBlue()
                .CreateNew(withFieldRestrictions);
        }
    }
}
