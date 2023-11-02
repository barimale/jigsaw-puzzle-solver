using TypeGen.Core.TypeAnnotations;

namespace Algorithm.ConstraintsPairing.Model.Responses
{
    [ExportTsInterface]
    public class AlgorithmResponse
    {
        public bool IsError { get; set; } = false;
        public string Reason { get; set; }
        public Pair[] Pairs { get; set; }
        public string AnalysisStatus { get; set; }
    }
}
