using GeneticSharp;
using GeneticSharp.Extensions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.Factory
{
    public class SolverBuilder
    {
        private IPopulation population;
        private ISelection selection;
        private ICrossover crossover;
        private IFitness fitness;
        private IMutation mutation;

        public SolverBuilder WithMutation(IMutation mutation)
        {
            this.mutation = mutation;

            return this;
        }

        public SolverBuilder WithCrossover(ICrossover crossover)
        {
            this.crossover = crossover;

            return this;
        }

        public SolverBuilder WithFitnessFunction(IFitness fitness)
        {
            this.fitness = fitness;

            return this;
        }

        public SolverBuilder WithSelection(ISelection selection)
        {
            this.selection = selection;

            return this;
        }

        public SolverBuilder WithPopulation(IPopulation population)
        {
            this.population = population;

            return this;
        }

        public GeneticAlgorithm Build()
        {
            return new GeneticAlgorithm(
                this.population,
                this.fitness,
                this.selection,
                this.crossover,
                this.mutation);
        }
    }
}
