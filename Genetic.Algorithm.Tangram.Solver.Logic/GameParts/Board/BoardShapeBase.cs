﻿using NetTopologySuite.Algorithm;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Utilities;
using NetTopologySuite.Operation.Union;

namespace Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board
{
    public class BoardShapeBase
    {
        public IList<BoardFieldDefinition> BoardFieldsDefinition { private set; get; }
        public int WidthUnit { private set; get; }
        public int HeightUnit { private set; get; }
        public int ScaleFactor { private set; get; } = 1;
        public int Width => WidthUnit * ScaleFactor;
        public int Height => HeightUnit * ScaleFactor;
        public Polygon Polygon { private set; get; }
        public Polygon CoverableBoard { private set; get; }
        public double Area => Polygon.Area;

        public BoardShapeBase(

            IList<BoardFieldDefinition> boardFieldsDefinition,
            int widthUnit,
            int heightUnit,
            int scaleFactor)
        {
            BoardFieldsDefinition = boardFieldsDefinition;
            Polygon = MapFieldsToPolygon();
            CoverableBoard = AsCoverableBoard();
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

        private Polygon AsCoverableBoard()
        {
            var scaleF = (double)((ScaleFactor / 20d + 1d));
            var transform = new AffineTransformation();
            var rotation = transform.Scale(scaleF, scaleF);

            var newGeometry = new GeometryFactory()
                    .CreatePolygon(Polygon.Coordinates.ToArray());

            newGeometry.Apply(rotation);

            return newGeometry;
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
            return new string[1] { this.ToString() };
        }
    }
}
