using TypeGen.Core.TypeAnnotations;

namespace Tangram.Solver.UI.Domain
{
    [ExportTsInterface]
    public class PolygonPairsResult
    {
        public List<Polygon> blocks = new List<Polygon>();
    }
}