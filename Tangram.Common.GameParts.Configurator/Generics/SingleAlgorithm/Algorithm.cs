using Algorithm.Tangram.TreeSearch.Logic;
using GeneticSharp;
using Solver.Tangram.AlgorithmDefinitions.Generics.Statistics;
using TreesearchLib;

namespace Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm
{
    public abstract class Algorithm<T> : IExecutableAlgorithm
        where T : class
    {
        public readonly int DEFAULT_AMOUNT_OF_CYCLES = 10;

        public int CurrentIteration { protected set; get; }

        protected T algorithm;

        public event EventHandler QualityCallback;
        public event EventHandler OnExecutionEstimationReady;

        private int cumulativeExecutionTimeInMiliseconds;

        public Algorithm(T algorithm)
        {
            this.algorithm = algorithm;
            this.Id = Guid.NewGuid().ToString();
            this.StatisticSettings = new StatisticSettings(DEFAULT_AMOUNT_OF_CYCLES);
        }

        public StatisticSettings StatisticSettings { get; set; }
        public string Id { private set; get; }

        // TODO: check if it needs to be cumulative or directly the value
        public void HandleExecutionEstimationCallback(
            ISearchControl<FindFittestSolution, Minimize> state,
            int maximalAmountOfIterations)
        {
            if (OnExecutionEstimationReady != null)
            {
                if(CurrentIteration % StatisticSettings.MeasurePeriodInCycles == 0)
                {
                    OnExecutionEstimationReady.Invoke(
                        new StatisticDetails(
                            this.Id,
                            maximalAmountOfIterations,
                            this.CurrentIteration)
                        {
                            MeanExecutionTimeOfIterationInMiliseconds = cumulativeExecutionTimeInMiliseconds / StatisticSettings.MeasurePeriodInCycles
                        },
                        null);

                    cumulativeExecutionTimeInMiliseconds = 0;
                }else
                {
                    cumulativeExecutionTimeInMiliseconds += state.Elapsed.Milliseconds;
                }
            }
        }

        // TODO: check if it needs to be cumulative or directly the value
        public void HandleExecutionEstimationCallback(
            GeneticAlgorithm state)
        {
            if (OnExecutionEstimationReady != null)
            {
                if(CurrentIteration % StatisticSettings.MeasurePeriodInCycles == 0)
                {
                    OnExecutionEstimationReady.Invoke(
                        new StatisticDetails(this.Id)
                        {
                            MeanExecutionTimeOfIterationInMiliseconds = cumulativeExecutionTimeInMiliseconds / StatisticSettings.MeasurePeriodInCycles
                        },
                        null);

                    cumulativeExecutionTimeInMiliseconds = 0;
                }else
                {
                    cumulativeExecutionTimeInMiliseconds += state.TimeEvolving.Milliseconds;
                }
            }
        }

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
            GeneticAlgorithm state)
        {
            if (QualityCallback != null)
            {
                QualityCallback.Invoke(
                    new AlgorithmResult()
                    {
                        Fitness = state.Fitness.ToString(),
                        Solution = state.BestChromosome,
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
