using NetTopologySuite.Algorithm;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Utilities;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks
{
    public class BlockBase
    {
        public Geometry Polygon { private set; get; }
        public Color Color { private set; get; }

        public double Area => Polygon.Area;

        public BlockBase(Geometry polygon, Color color)
        {
            Polygon = polygon;
            Color = color;
            this.MoveToZero();
        }

        public BlockBase Clone()
        {
            return new BlockBase(Polygon, Color);
        }

        public void Rotate(double angleDegrees)
        {
            var centroid = Polygon.Centroid;

            var transform = new AffineTransformation();
            var rotation = transform
                .Rotate(
                    AngleUtility.ToRadians(angleDegrees),
                    centroid.X,
                    centroid.Y
                );

            Polygon.Apply(rotation);
            this.MoveToZero();// maybe not necessary
        }

        // reflection / mirror
        public void Reflection()
        {
            var minAndMaxXYs = Polygon.Boundary.EnvelopeInternal;
            var transform = new AffineTransformation();
            var mirror = transform
                .Reflect(
                    (minAndMaxXYs.MaxX - minAndMaxXYs.MinX) / 2.0d,
                    minAndMaxXYs.MaxY,
                    (minAndMaxXYs.MaxX - minAndMaxXYs.MinX) / 2.0d,
                    minAndMaxXYs.MinY);

            Polygon.Apply(mirror);
            this.MoveToZero();// maybe not necessary
        }

        //move to the (0, 0)
        public void MoveToZero()
        {
            var transformToZeroZero = new AffineTransformation();
            var moveToZero = transformToZeroZero
                .Translate(
                    -Polygon.Boundary.EnvelopeInternal.MinX,
                    -Polygon.Boundary.EnvelopeInternal.MinY
                );

            Polygon.Apply(moveToZero);
        }

        public void MoveTo(int x, int y)
        {
            var transform = new AffineTransformation();
            var moveTo = transform
                .Translate(
                    x,
                    y
                );

            Polygon.Apply(moveTo);
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
