using SixLabors.Shapes;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks
{
    public class BlockBase
    {
        public RectangularPolygon Polygon { private set; get; }
        public Color Color { private set; get; }

        public BlockBase(RectangularPolygon polygon, Color color)
        {
            Polygon = polygon;
            Color = color;
        }

        public BlockBase Clone()
        {
            return new BlockBase(Polygon, Color);
        }
    }
}
