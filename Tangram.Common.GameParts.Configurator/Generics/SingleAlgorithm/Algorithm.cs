using Algorithm.Tangram.TreeSearch.Logic;
using GeneticSharp;
using TreesearchLib;

namespace Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm
{
    public abstract class Algorithm<T> : IExecutableAlgorithm
        where T : class
    {
        protected T algorithm;

        public event EventHandler QualityCallback;

        public Algorithm(T algorithm)
        {
            this.algorithm = algorithm;
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { private set; get; }

        public void HandleQualityCallback(
            ISearchControl<FindFittestSolution, Minimize> state)
        {
            if (QualityCallback != null)
            {
                QualityCallback.Invoke(
                    new AlgorithmResult()
                    {
                        Fitness = state.BestQuality.Value.Value.ToString(),
                        Solution = state.BestQualityState,
                        IsError = false
                    },
                    null);
            }
            else
            {
                // do nothing
            }
        }

        public void HandleQualityCallback(
            GeneticAlgorithm ga)
        {
            if (QualityCallback != null)
            {
                QualityCallback.Invoke(
                    new AlgorithmResult()
                    {
                        Fitness = ga.Fitness.ToString(),
                        Solution = ga.BestChromosome,
                        IsError = false
                    },
                    null);
            }
            else
            {
                // do nothing
            }
        }

        public abstract Task<AlgorithmResult> ExecuteAsync(CancellationToken ct = default);
    }
}
