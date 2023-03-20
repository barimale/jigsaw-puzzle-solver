using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;
using System.Collections.Immutable;

namespace Solver.Tangram.AlgorithmDefinitions.Generics
{
    public class MultiAlgorithm<T> : IExecutableMultiAlgorithm
        where T : IExecutableAlgorithm
    {
        private readonly ImmutableList<T> algorithms;
        private readonly ExecutionMode executionMode;

        public MultiAlgorithm(
            ExecutionMode executionMode,
            IList<T> algorithms)
        {
            this.algorithms = algorithms.ToImmutableList();
            this.executionMode = executionMode;
        }

        public MultiAlgorithm(
            ExecutionMode executionMode,
            params T[] algorithms)
        {
            this.executionMode = executionMode;
            this.algorithms = algorithms.ToImmutableList();
        }

        public event EventHandler GenerationRan; // of func / action here
        public event EventHandler TerminationReached; // of func / action here

        public async Task<IList<AlgorithmResult>> ExecuteManyAsync(CancellationToken ct = default)
        {
            var allOfThem = algorithms
                    .Select(p => Task
                            .Run(async () => await p.ExecuteAsync(), ct))
                    .ToImmutableArray();

            switch (executionMode)
            {
                case ExecutionMode.WhenAll:
                    var results = await Task
                        .WhenAll(allOfThem);

                    return results
                        .ToList();
                case ExecutionMode.WhenAny:
                    var result = await Task
                        .WhenAny(allOfThem);

                    return new List<AlgorithmResult>()
                    {
                        result.Result
                    };
                default:
                    throw new ArgumentException("ExecutionMode not recognized");
            }
        }
    }
}
