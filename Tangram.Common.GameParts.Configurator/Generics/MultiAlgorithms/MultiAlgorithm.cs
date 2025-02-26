using Solver.Tangram.AlgorithmDefinitions.Generics.EventArgs;
using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;
using System.Collections.Immutable;

namespace Solver.Tangram.AlgorithmDefinitions.Generics.MultiAlgorithms
{
    public class MultiAlgorithm : IExecutableMultiAlgorithm
    {
        private readonly ImmutableDictionary<string, IExecutableAlgorithm> algorithms;
        private readonly ExecutionMode executionMode;

        public MultiAlgorithm(
            ExecutionMode executionMode,
            IList<IExecutableAlgorithm> algorithms)
        {
            if (algorithms == null) throw new ArgumentNullException(nameof(algorithms));

            this.executionMode = executionMode;
            this.algorithms = algorithms.ToImmutableDictionary(p => p.Id, p => p);
        }

        public MultiAlgorithm(
            ExecutionMode executionMode,
            params IExecutableAlgorithm[] algorithms)
        {
            if (algorithms == null) throw new ArgumentNullException(nameof(algorithms));

            this.executionMode = executionMode;
            this.algorithms = algorithms.ToImmutableDictionary(p => Guid.NewGuid().ToString(), p => p);
        }

        public ImmutableDictionary<string, IExecutableAlgorithm> Algorithms => algorithms;

        public event EventHandler<SourceEventArgs> QualityCallback;
        public event EventHandler OnExecutionEstimationReady;

        public async Task<AlgorithmResult[]> ExecuteManyAsync(CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();

            var allOfThem = algorithms
                .Values
                .Select(p =>
                {
                    p.QualityCallback += QualityCallback;
                    p.OnExecutionEstimationReady += OnExecutionEstimationReady;
                    return p.ExecuteAsync(ct);
                })
                .ToImmutableArray();

            switch (executionMode)
            {
                case ExecutionMode.WhenAll:
                    var results = await Task.WhenAll(allOfThem);
                    return results.ToArray();
                case ExecutionMode.WhenAny:
                    var result = await Task.WhenAny(allOfThem);
                    return new AlgorithmResult[] { await result };
                default:
                    throw new ArgumentException("ExecutionMode not recognized");
            }
        }
    }
}