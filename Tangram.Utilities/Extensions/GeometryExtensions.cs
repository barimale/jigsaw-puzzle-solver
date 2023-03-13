using NetTopologySuite.Geometries;

namespace Genetic.Algorithm.Tangram.Common.Extensions
{
    public static class GeometryExtensions
    {
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
    }
}
