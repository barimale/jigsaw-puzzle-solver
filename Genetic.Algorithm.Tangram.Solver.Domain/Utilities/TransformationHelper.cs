using NetTopologySuite.Algorithm;
using NetTopologySuite.Geometries.Utilities;
using NetTopologySuite.Geometries;
using Algorithm.Tangram.Common.Extensions;

namespace Tangram.GameParts.Logic.Utilities
{
    // TODO: reuse later on when IOC in place
    public class TransformationHelper
    {
        public Geometry Rotate(double angleDegrees, Geometry polygon)
        {
            var centroid = polygon.Centroid;

            var transform = new AffineTransformation();
            var rotation = transform
                .Rotate(
                    AngleUtility.ToRadians(angleDegrees),
                    centroid.X,
                    centroid.Y
                );

            polygon.Apply(rotation);
            polygon = MoveToZero(polygon);
            polygon.CleanCoordinateDigits();

            return polygon;
        }

        // reflection / mirror
        public Geometry Reflection(Geometry polygon)
        {
            var minAndMaxXYs = polygon.Boundary.EnvelopeInternal;
            var transform = new AffineTransformation();
            var mirror = transform
                .Reflect(
                    (minAndMaxXYs.MaxX - minAndMaxXYs.MinX) / 2.0d,
                    minAndMaxXYs.MaxY,
                    (minAndMaxXYs.MaxX - minAndMaxXYs.MinX) / 2.0d,
                    minAndMaxXYs.MinY);

            polygon.Apply(mirror);
            polygon = MoveToZero(polygon);
            polygon.CleanCoordinateDigits();

            return polygon;
        }

        public void Apply(ref Geometry polygon, Geometry template)
        {
            polygon = new GeometryFactory().CreatePolygon(template.Coordinates);
        }

        //move to the (0, 0)
        public Geometry MoveToZero(Geometry polygon)
        {
            var transformToZeroZero = new AffineTransformation();
            var moveToZero = transformToZeroZero
                .Translate(
                    -polygon.EnvelopeInternal.MinX,
                    -polygon.EnvelopeInternal.MinY
                );

            polygon.Apply(moveToZero);
            polygon.CleanCoordinateDigits();

            return polygon;
        }

        public Geometry MoveTo(int x, int y, Geometry polygon)
        {
            var transform = new AffineTransformation();
            var moveTo = transform
                .Translate(
                    x,
                    y
                );

            polygon.Apply(moveTo);
            polygon.CleanCoordinateDigits();

            return polygon;
        }
    }
}
