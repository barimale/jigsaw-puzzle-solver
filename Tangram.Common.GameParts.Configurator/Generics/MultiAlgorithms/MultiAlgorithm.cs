using Solver.Tangram.AlgorithmDefinitions.Generics.EventArgs;
using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;
using System.Collections.Immutable;

namespace Solver.Tangram.AlgorithmDefinitions.Generics.MultiAlgorithms
{
    public class MultiAlgorithm : IExecutableMultiAlgorithm
    {
        private readonly Dictionary<string, IExecutableAlgorithm> algorithms = new Dictionary<string, IExecutableAlgorithm>();
        private readonly ExecutionMode executionMode;

        public MultiAlgorithm(
            ExecutionMode executionMode,
            IList<IExecutableAlgorithm> algorithms)
        {
            this.executionMode = executionMode;
            algorithms.ToList().ForEach(p =>
            {
                this.algorithms.TryAdd(p.Id, p);
            });
        }

        public MultiAlgorithm(
            ExecutionMode executionMode,
            params IExecutableAlgorithm[] algorithms)
        {
            this.executionMode = executionMode;
            algorithms.ToList().ForEach(p =>
            {
                this.algorithms.TryAdd(Guid.NewGuid().ToString(), p);
            });
        }

        public Dictionary<string, IExecutableAlgorithm> Algorithms => algorithms;

        public event EventHandler<SourceEventArgs> QualityCallback; // of func / action here
        public event EventHandler OnExecutionEstimationReady;

        public async Task<AlgorithmResult[]> ExecuteManyAsync(CancellationToken ct = default)
        {

            ct.ThrowIfCancellationRequested();

            var allOfThem = algorithms
                    .Keys
                    .Select(pp =>
                    {
                        algorithms[pp].QualityCallback += QualityCallback;
                        algorithms[pp].OnExecutionEstimationReady += OnExecutionEstimationReady;

                        return algorithms[pp];
                    })
                    .Select(p => Task
                            .Run(async () => await p.ExecuteAsync(ct), ct))
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
