using Genetic.Algorithm.Tangram.Common.Extensions;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using NetTopologySuite.Algorithm;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Utilities;

namespace Genetic.Algorithm.Tangram.Solver.Domain.Extensions
{
    public static class BoardFieldDefinitionCollectionExtensions
    {
        public static GeometryCollection? ConvertToGeometryCollection(
            this IList<BoardFieldDefinition>? fields)
        {
            var geometricies = fields.Select(p =>
            {
                var polygonWithData = new GeometryFactory()
                        .CreatePolygon(p.ToCoordinates());
                polygonWithData.UserData = p.FieldRestrictionMarkup;

                // TODO: check userdata in geometry
                var polygonAsGeometry = new GeometryFactory()
                    .CreateGeometry(polygonWithData);

                return polygonAsGeometry;
                // polygonAsGeometry.UserData
            }).ToArray();

            var geometryCollection = new GeometryFactory()
                .CreateGeometryCollection(geometricies);

            return geometryCollection;
        }


        public static void Rotate(this GeometryCollection collection, double angleDegrees)
        {
            var centroid = collection.Centroid;

            var transform = new AffineTransformation();
            var rotation = transform
                .Rotate(
                    AngleUtility.ToRadians(angleDegrees),
                    centroid.X,
                    centroid.Y
                );

            collection.Apply(rotation);
            collection.MoveToZero();
            collection.CleanCoordinateDigits();
        }

        // reflection / mirror
        public static void Reflection(this GeometryCollection collection)
        {
            var minAndMaxXYs = collection.Boundary.EnvelopeInternal;
            var transform = new AffineTransformation();
            var mirror = transform
                .Reflect(
                    (minAndMaxXYs.MaxX - minAndMaxXYs.MinX) / 2.0d,
                    minAndMaxXYs.MaxY,
                    (minAndMaxXYs.MaxX - minAndMaxXYs.MinX) / 2.0d,
                    minAndMaxXYs.MinY);

            collection.Apply(mirror);
            collection.MoveToZero();
            collection.CleanCoordinateDigits();
        }

        //move to the (0, 0)
        private static void MoveToZero(this GeometryCollection collection)
        {
            var transformToZeroZero = new AffineTransformation();
            var moveToZero = transformToZeroZero
                .Translate(
                    -collection.Boundary.EnvelopeInternal.MinX,
                    -collection.Boundary.EnvelopeInternal.MinY
                );

            collection.Apply(moveToZero);
            collection.CleanCoordinateDigits();
        }
    }
}