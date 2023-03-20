using NetTopologySuite.Geometries;

namespace Generic.Algorithm.Tangram.Common.Extensions
{
    public static class GeometryExtensions
    {
        // to have the coordinates prepared in a form of copable string to use here:
        // https://www.mathsisfun.com/geometry/polygons-interactive.html
        // (copy-paste to the modal window shown on click the edit button)
        public static string[] ToDrawerString(this IEnumerable<Geometry> input)
        {
            return input
                .Select(p =>
                {
                    var toString = p
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
                })
                .ToArray();
        }

        public static Geometry? WithPolishedCoordinates(this Geometry? geometry)
        {
            if (geometry == null)
                return geometry;

            var digits = 3;
            var minimalDiff = 0.001d;

            foreach (var cc in geometry.Coordinates)
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

            return geometry;
        }

        public static void CleanCoordinateDigits(this Geometry? geometry)
        {
            if (geometry == null)
                return;

            var digits = 3;
            var minimalDiff = 0.001d;

            foreach (var cc in geometry.Coordinates)
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
    }
}
