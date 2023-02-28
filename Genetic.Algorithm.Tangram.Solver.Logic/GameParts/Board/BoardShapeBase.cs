using NetTopologySuite.Geometries;

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
            WidthUnit = widthUnit;
            HeightUnit = heightUnit;
            ScaleFactor = scaleFactor;
        }

        private Polygon MapFieldsToPolygon()
        {
            var coordinates = BoardFieldsDefinition
                .Select(p => p.ToCoordinate())
                .ToArray();

            // add first as last to have the geometry closed
            coordinates.Append(coordinates[0]);

            var polygon = new GeometryFactory()
                .CreatePolygon(coordinates);

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
    }
}
