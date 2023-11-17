using TypeGen.Core.TypeAnnotations;

namespace Tangram.Solver.UI.Domain
{
    [ExportTsInterface]
    public class AlgorithmDetails
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}