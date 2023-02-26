using GeneticSharp;

namespace Console
{
    class Program
    {
        /// <summary>
        /// GeneticSharp Console Application template.
        /// <see href="https://github.com/giacomelli/GeneticSharp"/>
        /// </summary>
        static void Main(string[] args)
        {
            // TODO: use the best genetic algorithm operators to your optimization problem.
            var selection = new EliteSelection();
            var crossover = new UniformCrossover();
            var mutation = new UniformMutation(true);

            var fitness = new MyProblemFitness();
            var chromosome = new MyProblemChromosome();

            var population = new Population(50, 70, chromosome);

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new FitnessStagnationTermination(100);
            ga.GenerationRan += (s, e) => System.Console.WriteLine($"Generation {ga.GenerationsNumber}. Best fitness: {ga.BestChromosome.Fitness.Value}");

            System.Console.WriteLine("GA running...");
            ga.Start();

            System.Console.WriteLine();
            System.Console.WriteLine($"Best solution found has fitness: {ga.BestChromosome.Fitness}");
            System.Console.WriteLine($"Elapsed time: {ga.TimeEvolving}");
            System.Console.ReadKey();
        }
    }
}
