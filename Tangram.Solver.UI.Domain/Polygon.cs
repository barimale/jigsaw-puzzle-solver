using TypeGen.Core.TypeAnnotations;

namespace Tangram.Solver.UI.Domain
{
    [ExportTsInterface]
    public class Polygon
    {
        public List<PolygonPair> polygonPairs = new List<PolygonPair>();
        public string color = null!;
    }
}