using GeneticSharp;

namespace Genetic.Algorithm.Tangram.Solver.Logic.Factory
{
    public class SolverBuilder
    {
        private Population population;

        public SolverBuilder WithPopulation(Population population)
        {
            this.population = population;

            return this;
        }

        public GeneticAlgorithm Build()
        {
            //var selection = new EliteSelection();
            //var crossover = new OrderedCrossover();
            //var mutation = new ReverseSequenceMutation();
            //var fitness = new TspFitness(100, 0, 1000, 0, 1000);

            return new GeneticAlgorithm(
                this.population,
                fitness,
                selection,
                crossover,
                mutation);
        }
    }
}
