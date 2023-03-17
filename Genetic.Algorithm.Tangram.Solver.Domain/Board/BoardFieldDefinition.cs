using NetTopologySuite.Geometries;

namespace Genetic.Algorithm.Tangram.Solver.Domain.Board
{
    // TODO: rename it to be common for board and blocks

    /// <summary>
    ///  three slashes gives You summary
    /// </summary>
    public class BoardFieldDefinition
    {
        public bool IsExtraRistricted { private set; get; }
        public object FieldRestrictionMarkup { private set; get; }

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
            IsExtraRistricted = false;
        }

        public BoardFieldDefinition(
            int x,
            int y,
            double width,
            double height,
            object fieldRestrictionMarkup,
            bool isEnabled = true,
            int scaleFactor = 1)
            : this(x, y, width, height, isEnabled, scaleFactor)
        {
            IsExtraRistricted = true;
            FieldRestrictionMarkup = fieldRestrictionMarkup;
        }

        public Coordinate[] ToCoordinates()
        {
            var coordinates = new List<Coordinate>()
            {
                new Coordinate(X, Y),
                new Coordinate(X + Width, Y),
                new Coordinate(X + Width, Y + Height),
                new Coordinate(X, Y + Height),
                new Coordinate(X, Y)
            };

            return coordinates.ToArray();
        }
    }
}
