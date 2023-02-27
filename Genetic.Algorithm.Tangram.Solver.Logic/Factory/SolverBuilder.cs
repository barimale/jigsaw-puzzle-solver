using GeneticSharp;

namespace Genetic.Algorithm.Tangram.Solver.Logic.Factory
{
    public class SolverBuilder
    {
        private IPopulation population;
        private ISelection selection;
        private ICrossover crossover;
        private IFitness fitness;
        private IMutation mutation;
        private ITermination termination;
        private IReinsertion reinsertion;

        //TODO: check if it is an equivalent of the preloadPopulation method
        // as presented in the Issue9 sample
        public SolverBuilder WithReinsertion(IReinsertion reinsertion)
        {
            this.reinsertion = reinsertion;

            return this;
        }

        public SolverBuilder WithTermination(ITermination termination)
        {
            this.termination = termination;

            return this;
        }

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
            var ga = new GeneticAlgorithm(
                this.population,
                this.fitness,
                this.selection,
                this.crossover,
                this.mutation);

            ga.Termination = this.termination;
            ga.Reinsertion = this.reinsertion;

            return ga;
        }
    }
}
