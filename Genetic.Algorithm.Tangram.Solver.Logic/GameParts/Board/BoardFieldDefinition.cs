using NetTopologySuite.Geometries;

namespace Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board
{
    public class BoardFieldDefinition
    {
        public bool IsEnabled { private set; get; }
        public int ScaleFactor { private set; get; }

        public int X { private set; get; }
        public int Y { private set; get; }

        public BoardFieldDefinition(
            int x,
            int y,
            bool isEnabled = true,
            int scaleFactor = 1)
        {
            IsEnabled = isEnabled;
            ScaleFactor = scaleFactor;
            X = x;
            Y = y;
        }

        public Coordinate ToCoordinate()
        {
            return new Coordinate(this.X, this.Y);
        }
    }
}
