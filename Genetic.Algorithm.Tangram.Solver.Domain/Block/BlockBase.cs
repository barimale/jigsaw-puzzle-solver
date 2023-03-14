using NetTopologySuite.Algorithm;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Utilities;
using System.Drawing;

namespace Genetic.Algorithm.Tangram.Solver.Domain.Block
{
    public class BlockBase
    {
        public Guid ID { get; private set; }

        public Geometry Polygon { private set; get; }
        public Color Color { private set; get; }
        public Geometry[] AllowedLocations { private set; get; } = new Geometry[0];
        public bool IsAllowedLocationsEnabled => AllowedLocations != null && AllowedLocations.Length > 0;
        public double Area => Polygon.Area;

        public BlockBase(
            Geometry polygon,
            Color color,
            bool moveToZero = true)
        {
            this.ID = Guid.NewGuid();
            this.Polygon = polygon;
            this.Color = color;

            if (moveToZero)
            {
                MoveToZero();
            }
        }

        public void SetAllowedLocations(Geometry[] locations)
        {
            AllowedLocations = locations;
        }

        public BlockBase Clone(bool moveToZero = true)
        {
            var cloned = new BlockBase(Polygon, Color, moveToZero);
            cloned.SetAllowedLocations(AllowedLocations);

            return cloned;
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
            MoveToZero();
            CleanCoordinateDigits();
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
            MoveToZero();
            CleanCoordinateDigits();
        }

        public void Apply(Geometry template)
        {
            Polygon = template;
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
            CleanCoordinateDigits();
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
            CleanCoordinateDigits();
        }

        private void CleanCoordinateDigits()
        {
            var digits = 3;
            var minimalDiff = 0.001d;

            foreach (var cc in Polygon.Coordinates)
            {
                if (Math.Abs(cc.CoordinateValue.X - Math.Round(cc.CoordinateValue.X, digits, MidpointRounding.AwayFromZero)) < minimalDiff)
                {
                    var result = Convert.ToInt32(
                            Math.Round(cc.CoordinateValue.X, digits, MidpointRounding.AwayFromZero));
                    cc.CoordinateValue.X = result;
                }

                if (Math.Abs(cc.CoordinateValue.Y - Math.Round(cc.CoordinateValue.Y, digits, MidpointRounding.AwayFromZero)) < minimalDiff)
                {
                    var result = Convert.ToInt32(
                            Math.Round(cc.CoordinateValue.Y, digits, MidpointRounding.AwayFromZero));
                    cc.CoordinateValue.Y = result;
                }
            }
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

        public string[] BlockAsStringArray()
        {
            return new string[1] { ToString() };
        }
    }
}
