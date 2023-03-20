using Generic.Algorithm.Tangram.GameParts.Elements.Blocks.CommonSettings;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using NetTopologySuite.Geometries;
using System.Drawing;

namespace Generic.Algorithm.Tangram.GameParts.Elements.Blocks
{
    public sealed class DarkBlue : PolishGameBaseBlock
    {
        public DarkBlue()
        {
            fieldRestriction1side = new object[,] {
                { "O", "X", "O", "X", "O" }
            };

            fieldRestriction2side = new object[,] {
                { "X", "O", "X", "O", "X" }
            };

            color = Color.DarkBlue;

            polygon = new GeometryFactory()
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
