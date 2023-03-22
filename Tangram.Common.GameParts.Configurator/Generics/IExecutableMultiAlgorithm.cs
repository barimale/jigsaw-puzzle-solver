using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;

namespace Solver.Tangram.AlgorithmDefinitions.Generics
{
    public interface IExecutableMultiAlgorithm: IQualityCallback
    {
        public Task<AlgorithmResult[]> ExecuteManyAsync(CancellationToken ct = default);
    }
}
