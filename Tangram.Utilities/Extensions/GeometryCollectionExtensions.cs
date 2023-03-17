using NetTopologySuite.Algorithm;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Utilities;

namespace Genetic.Algorithm.Tangram.Common.Extensions
{
    public static class GeometryCollectionExtensions
    {
        public static void CleanCoordinateDigits(this GeometryCollection? geometryCollection)
        {
            if (geometryCollection == null)
                return;

            foreach(var geometry in geometryCollection)
            {
                geometry.CleanCoordinateDigits();
            }
        }

        public static void MoveTo(this GeometryCollection? collection, int x, int y)
        {
            if (collection == null)
                return;

            var transform = new AffineTransformation();
            var moveTo = transform
                .Translate(
                    x,
                    y
                );

            collection.Apply(moveTo);
            collection.CleanCoordinateDigits();
        }


        public static void Rotate(this GeometryCollection? collection, double angleDegrees)
        {
            if (collection == null)
                return;

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
        public static void Reflection(this GeometryCollection? collection)
        {
            if (collection == null)
                return;

            var minAndMaxXYs = collection.EnvelopeInternal;
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
        private static void MoveToZero(this GeometryCollection? collection)
        {
            if (collection == null)
                return;

            var transformToZeroZero = new AffineTransformation();
            var moveToZero = transformToZeroZero
                .Translate(
                    -collection.EnvelopeInternal.MinX,
                    -collection.EnvelopeInternal.MinY
                );

            collection.Apply(moveToZero);
            collection.CleanCoordinateDigits();
        }
    }
}
