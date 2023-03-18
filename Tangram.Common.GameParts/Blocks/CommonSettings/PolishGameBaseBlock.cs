using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using NetTopologySuite.Geometries;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.GameParts.Blocks.CommonSettings
{
    public abstract class PolishGameBaseBlock
    {
        public const string SkippedMarkup = "&&&";

        // shortcut to have sides described in a more readable way
        protected const string NA = SkippedMarkup;

        protected object[,]? fieldRestriction1side;
        protected object[,]? fieldRestriction2side;

        protected Color color;

        protected Polygon? polygon;

        public BlockBase CreateNew(bool withFieldRestrictions = false)
        {
            if (withFieldRestrictions)
            {
                var bloczek = new BlockBase(
                    polygon!,
                    color,
                    new[] { 
                        fieldRestriction1side!,
                        fieldRestriction2side!
                    });

                return bloczek;
            }
            else
            {
                var bloczek = new BlockBase(
                    polygon!,
                    color);

                return bloczek;
            }
        }
    }
}
