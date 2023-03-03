using Genetic.Algorithm.Tangram.Solver.Logic.UT.Data.BigBoard;
using Genetic.Algorithm.Tangram.Solver.Logic.Utilities;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.As_A_Developer
{
    public class I_would_like_to_solve_big_board
    {
        private AlgorithmDebugHelper AlgorithmResultsHelper = new AlgorithmDebugHelper();

        [Fact]
        public void With_shape_of_5x10_fields_by_using_10_blocks_and_no_unused_fields()
        {
            // given
            var konfiguracjaGry = BigBoardData.DemoData();

            konfiguracjaGry
                .Algorithm
                .TerminationReached += AlgorithmResultsHelper
                    .Algorithm_TerminationReached;
            
            konfiguracjaGry
                .Algorithm
                .GenerationRan += AlgorithmResultsHelper
                    .Algorithm_Ran;

            // when
            konfiguracjaGry.Algorithm.Start();

            // then
        }
    }
}