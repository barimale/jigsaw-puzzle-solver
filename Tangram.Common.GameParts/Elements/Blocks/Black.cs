using Genetic.Algorithm.Tangram.GameParts.Elements.Blocks.CommonSettings;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using NetTopologySuite.Geometries;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.GameParts.Elements.Blocks
{
    public sealed class Black : PolishGameBaseBlock
    {
        public Black()
        {
            fieldRestriction1side = new object[,] {
                { "O" }
            };

            fieldRestriction2side = new object[,] {
                { "X"}
            };

            color = Color.Blue;

            polygon = new GeometryFactory()
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
