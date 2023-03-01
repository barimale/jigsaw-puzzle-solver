using NetTopologySuite.Algorithm;
using NetTopologySuite.Geometries;
using NetTopologySuite.Operation.Union;

namespace Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board
{
    public class BoardShapeBase
    {
        public IList<BoardFieldDefinition> BoardFieldsDefinition { private set; get; }
        public int WidthUnit { private set; get; }
        public int HeightUnit { private set; get; }
        public int ScaleFactor { private set; get; } = 1;
        // use together with the scalefactor maybe
        public int Width => WidthUnit;
        public int Height => HeightUnit;
        public Polygon Polygon { private set; get; }
        public double Area => Polygon.Area;

        public BoardShapeBase(

            IList<BoardFieldDefinition> boardFieldsDefinition,
            int widthUnit,
            int heightUnit,
            int scaleFactor)
        {
            BoardFieldsDefinition = boardFieldsDefinition;
            Polygon = MapFieldsToPolygon();

            // TODO: has to be removed maybe together with the method ToString
            var asString = this.ToString();

            WidthUnit = widthUnit;
            HeightUnit = heightUnit;
            ScaleFactor = scaleFactor;
        }

        private Polygon MapFieldsToPolygon()
        {
            var boardPolygons = BoardFieldsDefinition
                .Select(p =>
                {
                    return new GeometryFactory()
                        .CreatePolygon(p.ToCoordinates());
                })
                .ToArray();

            var mergedPolygon = new CascadedPolygonUnion(boardPolygons)
                .Union();

            // add first as last to have the geometry closed
            //var aslastOne = new Coordinate(coordinates[0]);
            //coordinates = coordinates
            //    .Append(aslastOne)
            //    .ToArray();
            
            var polygon = new GeometryFactory()
                .CreatePolygon(mergedPolygon.Coordinates);

            return polygon;
        }

        public BoardShapeBase Clone()
        {
            return new BoardShapeBase(
                this.BoardFieldsDefinition,
                this.WidthUnit,
                this.HeightUnit,
                this.ScaleFactor);
        }

        public override string ToString()
        {
            var toString = Polygon
                .Coordinates
                .Select(p =>
                    "(" +
                    Math.Round(p.X, 2)
                        .ToString(System.Globalization.CultureInfo.InvariantCulture) +
                    "," +
                    Math.Round(p.Y, 2)
                        .ToString(System.Globalization.CultureInfo.InvariantCulture) +
                    ")").ToArray();
            var toStringAsArray = string.Join(',', toString);

            return toStringAsArray;
        }
    }
}
