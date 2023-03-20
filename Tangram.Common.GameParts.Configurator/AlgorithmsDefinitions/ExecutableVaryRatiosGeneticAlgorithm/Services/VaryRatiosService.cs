using Tangram.GameParts.Logic.GameParts;

namespace Solver.Tangram.AlgorithmDefinitions.AlgorithmsDefinitions.ExecutableVaryRatiosGeneticAlgorithm.Services
{
    internal class VaryRatiosService
    {
        private readonly int terminationGenerationsNumber;
        private int currentGeneration = 0;

        public event EventHandler? TerminationMaximalAmountOfGenerationsReached;
        public event EventHandler? OnRatiosChanged;

        public VaryRatiosService(int terminationGenerationsNumber)
        {
            this.terminationGenerationsNumber = terminationGenerationsNumber;
        }

        public float CrossoverProbability { private set; get; }
        public float MutationProbability { private set; get; }

        public void NextGeneration()
        {
            currentGeneration = currentGeneration + 1;

            CrossoverProbability = GetCrossoverProbabilityRatio();
            MutationProbability = GetMutationProbabilityRatio();

            if (currentGeneration > terminationGenerationsNumber)
            {
                TerminationMaximalAmountOfGenerationsReached?.Invoke(this, null);
            }
            else
            {
                OnRatiosChanged?.Invoke(this, null);
            }
        }

        public float GetCrossoverProbabilityRatio()
        {
            return GetDynamicRatios().Item2;
        }

        public float GetMutationProbabilityRatio()
        {
            return GetDynamicRatios().Item1;
        }

        private Tuple<float, float> GetDynamicRatios()
        {
            float factor = currentGeneration / (float)terminationGenerationsNumber;

            // mutation, crossover
            var ratios = new Tuple<float, float>(1f - factor, factor);

            return ratios;
        }
    }
}
