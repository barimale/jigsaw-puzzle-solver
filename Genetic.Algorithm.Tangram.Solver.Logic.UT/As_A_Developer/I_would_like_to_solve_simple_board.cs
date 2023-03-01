using Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities;
using Genetic.Algorithm.Tangram.Solver.Logic.Utilities;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.As_A_Developer
{
    public class I_would_like_to_solve_simple_board
    {
        private AlgorithmResultsHelper AlgorithmResultsHelper = new AlgorithmResultsHelper();

        [Fact]
        public void With_shape_of_2x2_fields_by_using_3_blocks_and_no_unused_fields()
        {
            // given
            var konfiguracjaGry = SimpleBoardData.DemoData();
            konfiguracjaGry.Algorithm.TerminationReached += AlgorithmResultsHelper.Algorithm_TerminationReached;
            konfiguracjaGry.Algorithm.GenerationRan += AlgorithmResultsHelper.Algorithm_Ran;

            // when
            konfiguracjaGry.Algorithm.Start();

            // then
        }
    }
}