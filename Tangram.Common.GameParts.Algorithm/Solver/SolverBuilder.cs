using GeneticSharp;

namespace Genetic.Algorithm.Tangram.AlgorithmSettings.Solver
{
    public class SolverBuilder
    {
        private IPopulation population;
        private ISelection selection;
        private ICrossover crossover;
        private IFitness fitness;
        private IMutation mutation;
        private ITermination? termination;
        private IReinsertion? reinsertion;
        private IOperatorsStrategy? operatorsStrategy;
        private ITaskExecutor? taskExecutor;
        private float? crossoverProbability;
        private float? mutationProbability;

        public SolverBuilder WithParallelTaskExecutor(
            int minThreads = 200,
            int maxThreads = 200
            // TODO WIP timeout here in seconds
            )
        {
            if(minThreads < 1)
                throw new ArgumentOutOfRangeException(nameof(minThreads));

            if(minThreads > maxThreads)
                throw new ArgumentOutOfRangeException(nameof(maxThreads));


            this.taskExecutor = new ParallelTaskExecutor()
            {
                MinThreads = minThreads,
                MaxThreads = maxThreads
            };

            return this;
        }

        public SolverBuilder WithTaskExecutor(ITaskExecutor taskExecutor)
        {
            this.taskExecutor = taskExecutor;

            return this;
        }

        public SolverBuilder WithOperatorsStrategy(IOperatorsStrategy operatorsStrategy)
        {
            this.operatorsStrategy = operatorsStrategy;

            return this;
        }

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

        public SolverBuilder WithMutation(IMutation mutation, float mutationProbability = 0.1f)
        {
            this.mutation = mutation;
            this.mutationProbability = mutationProbability;

            return this;
        }

        public SolverBuilder WithCrossover(ICrossover crossover, float? crossoverProbability = null)
        {
            this.crossover = crossover;
            this.crossoverProbability = crossoverProbability;

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
            if (population == null)
                throw new ArgumentNullException("population cannot be null");

            if (fitness == null)
                throw new ArgumentNullException("fitness cannot be null");

            if (selection == null)
                throw new ArgumentNullException("selection cannot be null");

            if (crossover == null)
                throw new ArgumentNullException("crossover cannot be null");

            if (mutation == null)
                throw new ArgumentNullException("mutation cannot be null");

            var ga = new GeneticAlgorithm(
                population,
                fitness,
                selection,
                crossover,
                mutation);

            if (termination != null)
                ga.Termination = termination;

            if (reinsertion != null)
                ga.Reinsertion = reinsertion;

            if (crossoverProbability != null &&
                crossoverProbability.HasValue)
                ga.CrossoverProbability = crossoverProbability.Value;

            if (mutationProbability != null &&
                mutationProbability.HasValue)
                ga.MutationProbability = mutationProbability.Value;

            if (operatorsStrategy != null)
                ga.OperatorsStrategy = operatorsStrategy;

            if (taskExecutor != null)
                ga.TaskExecutor = taskExecutor;

            return ga;
        }
    }
}
