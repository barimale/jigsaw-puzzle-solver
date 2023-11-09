using TypeGen.Core.TypeAnnotations;

namespace Christmas.Secret.Gifter.Domain
{
    [ExportTsInterface]
    public class PolygonPairsResult
    {
        public List<Polygon> blocks = new List<Polygon>();
    }
}