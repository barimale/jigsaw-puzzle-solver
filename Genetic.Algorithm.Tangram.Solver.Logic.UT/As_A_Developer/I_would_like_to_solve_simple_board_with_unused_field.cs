using Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities;
using Genetic.Algorithm.Tangram.Solver.Logic.Utilities;
using GeneticSharp;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.As_A_Developer
{
    // WIP
    public class I_would_like_to_solve_simple_board_with_unused_field
    {
        double latestFitness = double.MinValue;

        // TODO: implement it
        [Fact]
        public void With_shape_of_2x2_fields_by_using_3_blocks()
        {
            // given
            var konfiguracjaGry = SimpleBoardData.DemoData();

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
    }
}