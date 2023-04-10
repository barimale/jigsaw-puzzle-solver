using Algorithm.Tangram.Common.Extensions;
using NetTopologySuite.Algorithm;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Utilities;
using System.Data;
using System.Drawing;
using Tangram.GameParts.Logic.GameParts.Board;

namespace Tangram.GameParts.Logic.GameParts.Block
{
    public class BlockBase
    {
        public Guid ID { get; private set; }

        public bool IsExtraRistricted { private set; get; }
        public object[][,] FieldRestrictionMarkups { private set; get; }

        public Geometry Polygon { private set; get; }
        public Color Color { private set; get; }
        public Geometry[] AllowedLocations { private set; get; } = new Geometry[0];
        public bool IsAllowedLocationsEnabled => AllowedLocations != null
            && AllowedLocations.Length > 0;

        public double Area => Polygon.Area;

        public BlockBase(
            Geometry polygon,
            Color color,
            bool moveToZero = true)
        {
            ID = Guid.NewGuid();
            Polygon = polygon;
            Color = color;
            IsExtraRistricted = false;

            if (moveToZero)
            {
                MoveToZero();
            }
        }

        public BlockBase(
            Geometry polygon,
            Color color,
            object[][,] fieldRestrictionMarkups,
            bool moveToZero = true)
            : this(polygon, color, moveToZero)
        {
            IsExtraRistricted = true;
            FieldRestrictionMarkups = fieldRestrictionMarkups;
        }

        public void SetAllowedLocations(Geometry[] locations)
        {
            AllowedLocations = locations;
        }

        public int[] ToBinary(IList<BoardFieldDefinition> boardFieldsDefinition)
        {
            var result = boardFieldsDefinition
                .OrderBy(p => p.Y)
                .ThenBy(pp => pp.X)
                .Select(field =>
                    {
                        var fieldGeometry = new GeometryFactory()
                                .CreateGeometry(
                                    new GeometryFactory()
                                    .CreatePolygon(field.ToCoordinates()));

                        //var fieldGeometryWeb = ToWebsiteString(fieldGeometry);
                        //var polygonWeb = ToWebsiteString(Polygon);

                        var result = fieldGeometry.CoveredBy(Polygon);

                        if (result)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    })
                .ToArray();

            return result;
        }

        public BlockBase Clone(bool moveToZero = true)
        {
            var cloned = new BlockBase(
                new GeometryFactory().CreateGeometry(Polygon),
                Color,
                FieldRestrictionMarkups,
                moveToZero);

            cloned.SetAllowedLocations(AllowedLocations.ToArray());

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
            Polygon.CleanCoordinateDigits();
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
            Polygon.CleanCoordinateDigits();
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
                    -Polygon.EnvelopeInternal.MinX,
                    -Polygon.EnvelopeInternal.MinY
                );

            Polygon.Apply(moveToZero);
            Polygon.CleanCoordinateDigits();
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
            Polygon.CleanCoordinateDigits();
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

        private string ToWebsiteString(Geometry polygon)
        {
            var toString = polygon
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
