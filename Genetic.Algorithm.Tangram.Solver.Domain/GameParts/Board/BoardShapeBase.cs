using Algorithm.Tangram.Common.Extensions;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Utilities;
using NetTopologySuite.Operation.Union;

namespace Tangram.GameParts.Logic.GameParts.Board
{
    public class BoardShapeBase
    {
        // TODO: refactor it 
        public List<Tuple<string, int>>? AllowedMatches { set; get; }
        public string? SkippedMarkup { set; get; }
        public object[,] WithExtraRestrictedMarkups { set; get; }
        public bool IsExtraRistricted => AllowedMatches != null
            && AllowedMatches.Count > 0
            && WithExtraRestrictedMarkups.RowsCount() > 0
            && WithExtraRestrictedMarkups.ColumnsCount() > 0;

        public IList<BoardFieldDefinition> BoardFieldsDefinition { private set; get; }
        public int WidthUnit { private set; get; }
        public int HeightUnit { private set; get; }
        public int ScaleFactor { private set; get; } = 1;
        public int Width => WidthUnit * ScaleFactor;
        public int Height => HeightUnit * ScaleFactor;
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

            var polygon = new GeometryFactory()
                .CreatePolygon(mergedPolygon.Coordinates);

            polygon = MoveToZero(polygon);

            return polygon;
        }

        //move to the (0, 0)
        private Polygon MoveToZero(Polygon polygon)
        {
            var transformToZeroZero = new AffineTransformation();
            var moveToZero = transformToZeroZero
                .Translate(
                    -polygon.Boundary.EnvelopeInternal.MinX,
                    -polygon.Boundary.EnvelopeInternal.MinY
                );

            polygon.Apply(moveToZero);

            return polygon;
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

        public string[] BoardPolygonAsStringArray()
        {
            return new string[1] { ToString() };
        }
    }
}
