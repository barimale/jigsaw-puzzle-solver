using TypeGen.Core.TypeAnnotations;

namespace Tangram.Solver.UI.Domain
{
    [ExportTsInterface]
    public class GamePartsDetails
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
    }
}