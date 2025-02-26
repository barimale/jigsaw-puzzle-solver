using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;
using System.Collections.Immutable;

namespace Solver.Tangram.AlgorithmDefinitions.Generics.MultiAlgorithms
{
    public interface IExecutableMultiAlgorithm : IQualityCallback, IExecutionEstimationCallback
    {
        public Task<AlgorithmResult[]> ExecuteManyAsync(CancellationToken ct = default);
        public ImmutableDictionary<string, IExecutableAlgorithm> Algorithms { get; }
    }
}
