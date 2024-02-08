using NetTopologySuite.Geometries;
using System.Drawing;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Tangram.GameParts.Elements.Elements.Blocks.CommonSettings
{
    public abstract class PuzzleProBaseBlock
    {
        protected Color color;

        protected Polygon? polygon;

        public BlockBase CreateNew()
        {
            var bloczek = new BlockBase(
                     polygon!,
                     color);

            return bloczek;
        }
    }
}
