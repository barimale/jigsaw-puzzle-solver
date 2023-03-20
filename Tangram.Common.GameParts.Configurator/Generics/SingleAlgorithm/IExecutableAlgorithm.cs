using Solver.Tangram.AlgorithmDefinitions.Generics;

namespace Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm
{
    public interface IExecutableAlgorithm
    {
        public Task<AlgorithmResult> ExecuteAsync(CancellationToken ct = default);
    }
}
