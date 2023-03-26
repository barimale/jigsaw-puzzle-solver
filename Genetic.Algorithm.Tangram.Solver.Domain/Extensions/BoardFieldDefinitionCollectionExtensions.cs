using NetTopologySuite.Geometries;
using Tangram.GameParts.Logic.GameParts.Board;

namespace Tangram.GameParts.Logic.Extensions
{
    public static class BoardFieldDefinitionCollectionExtensions
    {
        public static GeometryCollection? ConvertToGeometryCollection(
            this IList<BoardFieldDefinition>? fields)
        {
            if (fields == null)
                return null;

            // do not use parallel here
            var geometricies = fields.Select(p =>
            {
                var polygonWithData = new GeometryFactory()
                        .CreatePolygon(p.ToCoordinates());

                var polygonAsGeometry = new GeometryFactory()
                        .CreateGeometry(polygonWithData);

                polygonAsGeometry.UserData = p.FieldRestrictionMarkup;

                return polygonAsGeometry;
            }).ToArray();

            var geometryCollection = new GeometryFactory()
                .CreateGeometryCollection(geometricies);

            return geometryCollection;
        }
    }
}