using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using GeneticSharp;
using Solver.Tangram.AlgorithmDefinitions.AlgorithmsDefinitions.ExecutableVaryRatiosGeneticAlgorithm.Services;
using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;

namespace Solver.Tangram.AlgorithmDefinitions.AlgorithmsDefinitions.ExecutableVaryRatiosGeneticAlgorithm
{
    public class ExecutableVaryRatiosGeneticAlgorithm : Algorithm<GeneticAlgorithm>, IExecutableAlgorithm
    {
        private const string NAME = "ExecutableVaryRatiosGeneticAlgorithm";

        private SemaphoreSlim signal = new SemaphoreSlim(0, 1);

        private AlgorithmResult result;
        private double latestFitness;

        private VaryRatiosService varyRatiosService;

        public ExecutableVaryRatiosGeneticAlgorithm(
            GeneticAlgorithm ga,
            int maximalGenerationAmount)
            : base(ga)
        {
            latestFitness = double.MinValue;
            result = new AlgorithmResult();

            this.varyRatiosService = new VaryRatiosService(maximalGenerationAmount);
            this.varyRatiosService.OnRatiosChanged += VaryRatiosService_OnRatiosChanged;
            this.varyRatiosService.TerminationMaximalAmountOfGenerationsReached += VaryRatiosService_TerminationMaximalAmountOfGenerationsReached;
        }

        public override string Name => NAME;
        public double LatestFitness => latestFitness;

        public override async Task<AlgorithmResult> ExecuteAsync(CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();

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

                base.HandleQualityCallback(algorithmResult);
                base.CurrentIteration += 1;
                base.HandleExecutionEstimationCallback(algorithmResult);

                varyRatiosService.NextGeneration();
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

        private void VaryRatiosService_TerminationMaximalAmountOfGenerationsReached(object? sender, EventArgs e)
        {
            // stop the flow
            algorithm.Stop();

            // unblock the executable method
            Algorithm_TerminationReached(algorithm, null);
        }

        private void VaryRatiosService_OnRatiosChanged(object? sender, EventArgs e)
        {
            try
            {
                var varyRatioInstance = sender as VaryRatiosService;

                if (varyRatioInstance == null)
                    return;

                algorithm.CrossoverProbability = varyRatioInstance.CrossoverProbability;
                algorithm.MutationProbability = varyRatiosService.MutationProbability;

            }
            catch (Exception)
            {
                // maybe invoke termination here
            }
        }
    }
}
