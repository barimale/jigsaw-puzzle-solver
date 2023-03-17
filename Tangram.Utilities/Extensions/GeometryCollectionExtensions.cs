using NetTopologySuite.Geometries;

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
    }
}
