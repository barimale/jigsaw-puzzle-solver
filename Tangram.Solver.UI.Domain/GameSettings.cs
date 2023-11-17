using TypeGen.Core.TypeAnnotations;

namespace Tangram.Solver.UI.Domain
{
    [ExportTsInterface]
    public class GameSettings
    {
        public string Id { get; set; } = null!;
        public string AlgorithmDetailsId { get; set; } = null!;
        public string GamePartsDetailsId { get; set; } = null!;
    }
}