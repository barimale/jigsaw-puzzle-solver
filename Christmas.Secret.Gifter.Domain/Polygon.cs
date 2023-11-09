using TypeGen.Core.TypeAnnotations;

namespace Christmas.Secret.Gifter.Domain
{
    [ExportTsInterface]
    public class Polygon
    {
        public List<PolygonPair> polygonPairs = new List<PolygonPair>();
        public string color = null!;
    }
}