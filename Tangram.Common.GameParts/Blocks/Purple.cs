using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using NetTopologySuite.Geometries;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.GameParts.Blocks
{
    public static class Purple
    {
        // move it later on to the base class or sth like this
        public const string SkippedMarkup = "&&&";
        private const string SM = SkippedMarkup;

        private static Color color = Color.Purple;

        private static Polygon polygon = new GeometryFactory()
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

        private static object[,] fieldRestriction1side = new object[,] { 
            { "O", SM, "O" },
            { "X", "O", "X" }
        };

        private static object[,] fieldRestriction2side = new object[,] {
            { "X", SM, "X" },
            { "O", "X", "O" }
        };

        public static BlockBase Create(bool withFieldRestrictions = false)
        {
            if(withFieldRestrictions)
            {
                var bloczek = new BlockBase(
                polygon,
                color,
                new[] { fieldRestriction1side, fieldRestriction2side });

                var punktyBloczkaDoNarysowania = bloczek
                    .ToString();

                return bloczek;
            }
            else
            {
                var bloczek = new BlockBase(
                polygon,
                color);

                var punktyBloczkaDoNarysowania = bloczek
                    .ToString();

                return bloczek;
            }
        }
    }
}
