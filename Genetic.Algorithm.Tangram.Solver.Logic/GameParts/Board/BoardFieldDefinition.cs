using NetTopologySuite.Geometries;

namespace Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board
{
    public class BoardFieldDefinition
    {
        public bool IsEnabled { private set; get; }
        public int ScaleFactor { private set; get; }

        public int X { private set; get; }
        public int Y { private set; get; }
        public double Width { private set; get; }
        public double Height { private set; get; }

        public BoardFieldDefinition(
            int x,
            int y,
            double width,
            double height,
            bool isEnabled = true,
            int scaleFactor = 1)
        {
            IsEnabled = isEnabled;
            ScaleFactor = scaleFactor;
            Width = width;
            Height = height;
            X = x;
            Y = y;
        }

        public Coordinate[] ToCoordinates()
        {
            var coordinates = new List<Coordinate>()
            {
                new Coordinate(this.X, this.Y),
                new Coordinate(this.X + this.Width, this.Y),
                new Coordinate(this.X + this.Width, this.Y + this.Height),
                new Coordinate(this.X, this.Y + this.Height),
                new Coordinate(this.X, this.Y)
            };

            return coordinates.ToArray();
        }
    }
}
