using Genetic.Algorithm.Tangram.Configurator;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Base;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities;
using Xunit.Abstractions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.As_A_Developer
{
    public class I_would_like_to_solve_big_board: PrintToConsoleUTBase
    {
        private AlgorithmDebugHelper AlgorithmDebugHelper;
        private AlgorithmUTConsoleHelper AlgorithmUTConsoleHelper;

        public I_would_like_to_solve_big_board(ITestOutputHelper output)
        : base(output)
        {
            AlgorithmDebugHelper = new AlgorithmDebugHelper();
            AlgorithmUTConsoleHelper = new AlgorithmUTConsoleHelper(output);
        }

        [Fact]
        public void With_shape_of_5x10_fields_by_using_10_blocks_and_no_unused_fields()
        {
            // given
            var gameParts = GamePartConfiguratorBuilder
                .AvalaibleGameSets
                .CreateBigBoard(withAllowedLocations: true);

            var algorithm = GamePartConfiguratorBuilder
                .AvalaibleGATunedAlgorithms
                .CreateBigBoardSettings(
                    gameParts.Board,
                    gameParts.Blocks,
                    gameParts.AllowedAngles);

            var konfiguracjaGry = new GamePartConfiguratorBuilder()
                .WithAlgorithm(algorithm)
                .WithGamePartsConfigurator(gameParts)
                .Build();

            if (konfiguracjaGry.Algorithm == null)
                return;

            konfiguracjaGry
                .Algorithm
                .TerminationReached += AlgorithmDebugHelper
                    .Algorithm_TerminationReached;
            
            konfiguracjaGry
                .Algorithm
                .GenerationRan += AlgorithmDebugHelper
                    .Algorithm_Ran;

            // when
            konfiguracjaGry.Algorithm.Start();

            // then
        }
    }
}