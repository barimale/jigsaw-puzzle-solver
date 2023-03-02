using GeneticSharp;

namespace Genetic.Algorithm.Tangram.Solver.Logic
{
    public class VaryRatioOperatorsStrategy : OperatorsStrategyBase
    {
        private int CurrentGeneration { get; set; } = -1;
        private int GenerationsNumber { get; set; } = -1;

        /// <summary>
        /// Crosses the specified parents.
        /// </summary>
        /// <param name="population">the population from which parents are selected</param>
        /// <param name="crossover">The crossover class.</param>
        /// <param name="crossoverProbability">The crossover probability.</param>
        /// <param name="parents">The parents.</param>
        /// <returns>The result chromosomes.</returns>
        public override IList<IChromosome> Cross(IPopulation population, ICrossover crossover, float crossoverProbability, IList<IChromosome> parents)
        {
            CurrentGeneration = population.CurrentGeneration.Number;
            GenerationsNumber = population.GenerationsNumber;
            crossoverProbability = GetCrossoverProbabilityRatio(crossoverProbability);

            var minSize = population.MinSize;
            var offspring = new List<IChromosome>(minSize);

            for (int i = 0; i < minSize; i += crossover.ParentsNumber)
            {
                var children = SelectParentsAndCross(population, crossover, crossoverProbability, parents, i);
                if (children != null)
                {
                    offspring.AddRange(children);
                }

            }

            return offspring;
        }

        /// <summary>
        /// Mutate the specified chromosomes.
        /// </summary>
        /// <param name="mutation">The mutation class.</param>
        /// <param name="mutationProbability">The mutation probability.</param>
        /// <param name="chromosomes">The chromosomes.</param>
        public override void Mutate(IMutation mutation, float mutationProbability, IList<IChromosome> chromosomes)
        {
            mutationProbability = GetMutationProbabilityRatio(mutationProbability);

            for (int i = 0; i < chromosomes.Count; i++)
            {
                mutation.Mutate(chromosomes[i], mutationProbability);
            }
        }

        private float GetCrossoverProbabilityRatio(float crossoverProbability)
        {
            if (CurrentGeneration < 0 || GenerationsNumber < 0)
                return crossoverProbability;

            return GetDynamicRatios().Item2;
        }

        private float GetMutationProbabilityRatio(float mutationProbability)
        {
            if (CurrentGeneration < 0 || GenerationsNumber < 0)
                return mutationProbability;

            return GetDynamicRatios().Item1;
        }

        private Tuple<float, float> GetDynamicRatios()
        {
            float factor = (float)CurrentGeneration / (float)GenerationsNumber;

            // mutation, crossover
            var ratios = new Tuple<float, float>(1f - factor, factor);

            return ratios;
        }
    }
}
