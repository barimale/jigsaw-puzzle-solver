using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;

namespace Solver.Tangram.AlgorithmDefinitions.Generics.MultiAlgorithms
{
    public interface IExecutableMultiAlgorithm : IQualityCallback, IExecutionEstimationCallback
    {
        public Task<AlgorithmResult[]> ExecuteManyAsync(CancellationToken ct = default);
        public Dictionary<string, IExecutableAlgorithm> Algorithms { get; }
    }
}
