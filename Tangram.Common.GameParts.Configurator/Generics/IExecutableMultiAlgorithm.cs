using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;

namespace Solver.Tangram.AlgorithmDefinitions.Generics
{
    public interface IExecutableMultiAlgorithm: IQualityCallback, IExecutionEstimationCallback
    {
        public Task<AlgorithmResult[]> ExecuteManyAsync(CancellationToken ct = default);
    }
}
