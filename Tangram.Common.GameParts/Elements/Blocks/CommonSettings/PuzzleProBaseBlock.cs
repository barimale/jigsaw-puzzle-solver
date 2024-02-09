using NetTopologySuite.Geometries;
using System.Drawing;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Tangram.GameParts.Elements.Elements.Blocks.CommonSettings
{
    public abstract class PuzzleProBaseBlock
    {
        public const string SkippedMarkup = "&&&";

        // shortcut to have sides described in a more compressed way
        // as it is goin to be used inside the array definition.
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
