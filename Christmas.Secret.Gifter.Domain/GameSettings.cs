using TypeGen.Core.TypeAnnotations;

namespace Christmas.Secret.Gifter.Domain
{
    [ExportTsInterface]
    public class GameSettings
    {
        public string Id { get; set; } = null!;
        public IEnumerable<AlgorithmDetails > AlgorithmDetails { get; set; } = new List<AlgorithmDetails>();
        public GamePartsDetails GamePartsDetails { get; set; }
    }
}