using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;
using System.Collections.Immutable;

namespace Solver.Tangram.AlgorithmDefinitions.Generics
{
    public class MultiAlgorithm: IExecutableMultiAlgorithm
    {
        private readonly ImmutableList<IExecutableAlgorithm> algorithms;
        private readonly ExecutionMode executionMode;

        public MultiAlgorithm(
            ExecutionMode executionMode,
            IList<IExecutableAlgorithm> algorithms)
        {
            this.algorithms = algorithms.ToImmutableList();
            this.executionMode = executionMode;
        }

        public MultiAlgorithm(
            ExecutionMode executionMode,
            params IExecutableAlgorithm[] algorithms)
        {
            this.executionMode = executionMode;
            this.algorithms = algorithms.ToImmutableList();
        }

        public event EventHandler QualityCallback; // of func / action here
        public event EventHandler OnExecutionEstimationReady;

        public async Task<AlgorithmResult[]> ExecuteManyAsync(CancellationToken ct = default)
        {
            var allOfThem = algorithms
                    .Select(pp =>
                    {
                        pp.QualityCallback += QualityCallback;
                        pp.OnExecutionEstimationReady += OnExecutionEstimationReady;

                        return pp;
                    })
                    .Select(p => Task
                            .Run(async () => await p.ExecuteAsync(), ct))
                    .ToImmutableArray();

            switch (executionMode)
            {
                case ExecutionMode.WhenAll:
                    var results = await Task
                        .WhenAll(allOfThem);

                    return results
                        .ToArray();
                case ExecutionMode.WhenAny:
                    var result = await Task
                        .WhenAny(allOfThem);

                    return new AlgorithmResult[]
                    {
                        result.Result
                    };
                default:
                    throw new ArgumentException("ExecutionMode not recognized");
            }
        }
    }
}
