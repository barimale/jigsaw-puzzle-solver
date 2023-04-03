using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using GeneticSharp;
using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;

namespace Solver.Tangram.AlgorithmDefinitions.AlgorithmsDefinitions
{
    public class ExecutableGeneticAlgorithm : Algorithm<GeneticAlgorithm>, IExecutableAlgorithm
    {
        private const string NAME = "ExecutableGeneticAlgorithm";

        private SemaphoreSlim signal = new SemaphoreSlim(0, 1);
        private AlgorithmResult result;
        private double latestFitness;

        public ExecutableGeneticAlgorithm(GeneticAlgorithm ga)
            : base(ga)
        {
            latestFitness = double.MinValue;
            result = new AlgorithmResult();
        }

        public string Name => NAME;
        public double LatestFitness => latestFitness;

        public override async Task<AlgorithmResult> ExecuteAsync(CancellationToken ct = default)
        {
            try
            {
                algorithm.GenerationRan += Algorithm_GenerationRan;
                algorithm.TerminationReached += Algorithm_TerminationReached;
                algorithm.Start();

                await signal.WaitAsync(ct);

                return result;
            }
            catch (Exception ex)
            {
                return new AlgorithmResult()
                {
                    IsError = true,
                    ErrorMessage = ex.Message
                };
            }
        }

        private void Algorithm_GenerationRan(object? sender, EventArgs e)
        {
            try
            {
                var algorithmResult = sender as GeneticAlgorithm;

                if (algorithmResult == null)
                    return;

                var bestChromosome = algorithmResult
                    .BestChromosome as TangramChromosome;

                if (bestChromosome == null || !bestChromosome.Fitness.HasValue)
                    return;

                var bestFitness = bestChromosome
                    .Fitness
                    .Value;

                if (bestFitness >= latestFitness)
                {
                    latestFitness = bestFitness;

                    base.HandleQualityCallback(algorithmResult);
                    base.CurrentIteration += 1;
                    base.HandleExecutionEstimationCallback(algorithmResult);
                }
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.ErrorMessage = ex.Message;
            }
        }

        private void Algorithm_TerminationReached(object? sender, EventArgs e)
        {
            try
            {
                var algorithmResult = sender as GeneticAlgorithm;

                if (algorithmResult != null)
                {
                    var bestChromosome = algorithmResult
                        .BestChromosome as TangramChromosome;

                    if (bestChromosome == null)
                    {
                        result.ErrorMessage = "BestChromosome is null";
                        result.IsError = true;
                        return;
                    }

                    result.Fitness = bestChromosome?.Fitness?.ToString();
                    result.Solution = bestChromosome;
                    result.IsError = false;
                }
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.ErrorMessage = ex.Message;
            }
            finally
            {
                signal.Release();
            }
        }
    }
}
