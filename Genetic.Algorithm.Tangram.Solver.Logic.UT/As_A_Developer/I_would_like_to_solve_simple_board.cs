using Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities;
using Genetic.Algorithm.Tangram.Solver.Logic.Utilities;
using GeneticSharp;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.As_A_Developer
{
    public class I_would_like_to_solve_simple_board
    {
        double latestFitness = double.MinValue;

        [Fact]
        public void With_shape_of_2x2_fields_by_using_3_blocks_and_no_unused_fields()
        {
            // given
            var konfiguracjaGry = DataHelper.SimpleBoardData();

            // when
            konfiguracjaGry.Algorithm.TerminationReached += Algorithm_TerminationReached;
            konfiguracjaGry.Algorithm.GenerationRan += Algorithm_Ran;

            // then
            konfiguracjaGry.Algorithm.Start();
        }

        private void Algorithm_Ran(object? sender, EventArgs e)
        {
            var algorithmResult = sender as GeneticAlgorithm;

            var bestChromosome = algorithmResult.BestChromosome as TangramChromosome;
            var bestFitness = bestChromosome.Fitness.Value;

            if (bestFitness >= latestFitness)
            {
                latestFitness = bestFitness;
                ConsoleHelper.ShowChromosome(algorithmResult.BestChromosome as TangramChromosome);
            }
        }

        private void Algorithm_TerminationReached(object? sender, EventArgs e)
        {
            var algorithmResult = sender as GeneticAlgorithm;

            Console.WriteLine("Best chromosome before chromossomes serialization is:");
            ConsoleHelper.ShowChromosome(algorithmResult.BestChromosome as TangramChromosome);
        }

        // TODO: field type X allowed or O allowed, and block types X or O.
        [Fact]
        public void With_shape_of_2x2_fields_by_using_3_blocks_and_two_types_of_fields()
        {
            // given
            
            // when
            
            // then
        }
    }
}