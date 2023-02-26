using Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities;
using Genetic.Algorithm.Tangram.Solver.Logic.Utilities;
using GeneticSharp.Extensions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.As_A_Developer
{
    public class I_would_like_to_solve_simple_board
    {
        [Fact]
        public void With_shape_of_2x2_fields_by_using_3_blocks_and_no_unused_fields()
        {
            // given
            
            // when
            
            // then
        }

        // TODO: field type X allowed or O allowed, and block types X or O.
        [Fact]
        public void With_shape_of_2x2_fields_by_using_3_blocks_and_two_types_of_fields()
        {
            // given
            var konfiguracjaGry = DataHelper.SimpleBoardData();

            // when
            konfiguracjaGry.Algorithm.Start();

            // then
            // TODO: think how many cycles are needed here, implement the wrapper
            Console.WriteLine("Best chromosome before chromossomes serialization is:");
            ConsoleHelper.ShowChromosome(konfiguracjaGry.Algorithm.BestChromosome as TspChromosome);
            // all suitable solutions are displayed in console
        }
    }
}