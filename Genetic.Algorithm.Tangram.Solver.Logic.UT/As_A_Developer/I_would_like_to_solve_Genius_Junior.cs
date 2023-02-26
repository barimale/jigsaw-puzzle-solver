using GeneticSharp.Extensions;
using GeneticSharp;
using Genetic.Algorithm.Tangram.Solver.Logic.Utilities;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.As_A_Developer
{
    public class I_would_like_to_solve_Genius_Junior
    {
        // TODO: probably ot has to be a wrapper containing some logic related with 
        // the management of population and how to reuse them (ELITISM ? )
        [Fact]
        public void With_shape_of_5x5_fields_by_using_3_types_of_blocks_and_no_unused_fields()
        {
            // given
            var size = Tuple.Create<int, int>(5, 5);
            // TODO: define shapes here
            //var blocks = new 
            //var blocks = 
            // TODO: modify it to use the custom chromosome class
            var chromosome = new TspChromosome(100);
            var population = new Population(50, 50, chromosome);
            var selection = new EliteSelection();
            var crossover = new OrderedCrossover();
            var mutation = new ReverseSequenceMutation();
            var fitness = new TspFitness(100, 0, 1000, 0, 1000);

            var solverBuilder = Factory.Factory.CreateNew();
            // TODO: add the rest of parameters to the builder and deeper
            var ga = solverBuilder
                .WithPopulation(population)
                .WithSelection(selection)
                .WithFitnessFunction(fitness)
                .WithMutation(mutation)
                .WithCrossover(crossover)
                .WithTermination(new GenerationNumberTermination(100))
                .Build();

            // when
            ga.Start();
            Console.WriteLine("Best chromosome before chromossomes serialization is:");
            ConsoleHelper.ShowChromosome(ga.BestChromosome as TspChromosome);
            ConsoleHelper.SerializeChromosomes(population.CurrentGeneration.Chromosomes);

            // Reload GA with serialized chromossomes from previous GA.
            Console.WriteLine("Deserializing...");
            var chromosomes = ConsoleHelper.DerializeChromosomes();

            // TODO: figure it out what for is that
            // Use a diff IPopulation implementation.
            var preloadPopulation = new PreloadedPopulation(50, 50, chromosomes);
            var ga_second = solverBuilder
               .WithPopulation(preloadPopulation)
               .WithSelection(selection)
               .WithFitnessFunction(fitness)
               .WithMutation(mutation)
               .WithCrossover(crossover)
               .Build();

            ga_second.Termination = new GenerationNumberTermination(1);
            ga_second.Start();
            Console.WriteLine("Best chromosome is:");
            ConsoleHelper.ShowChromosome(ga_second.BestChromosome as TspChromosome);
            // then
        }

        // later on correct shape as is in real game, define blocks with colors and handle unused fields feature
    }
}