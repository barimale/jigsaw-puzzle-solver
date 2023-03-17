using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using NetTopologySuite.Geometries;

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
    }
}
