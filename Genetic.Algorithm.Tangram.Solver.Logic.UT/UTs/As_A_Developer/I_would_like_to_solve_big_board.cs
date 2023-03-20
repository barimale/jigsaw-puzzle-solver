using Genetic.Algorithm.Tangram.Solver.Logic.Helpers;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.BaseUT;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Helpers;
using Solver.Tangram.AlgorithmDefinitions;
using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.Configuration;
using Xunit.Abstractions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.UTs.As_A_Developer
{
    public class I_would_like_to_solve_big_board : PrintToConsoleUTBase
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
        public async Task With_shape_of_5x10_fields_by_using_10_blocks_and_no_unused_fields()
        {
            // given
            var gameParts = GameConfiguratorBuilder
                .AvalaibleGameSets
                .CreateBigBoard(withAllowedLocations: true);

            var algorithm = GameConfiguratorBuilder
                .AvalaibleGATunedAlgorithms
                .CreateBigBoardSettings(
                    gameParts.Board,
                    gameParts.Blocks,
                    gameParts.AllowedAngles);

            var konfiguracjaGry = new GameConfiguratorBuilder()
                .WithAlgorithm(algorithm)
                .WithGamePartsConfigurator(gameParts)
                .Build();

            if (konfiguracjaGry.Algorithm == null)
                return;

            //konfiguracjaGry
            //    .Algorithm
            //    .TerminationReached += AlgorithmDebugHelper
            //        .Algorithm_TerminationReached;

            //konfiguracjaGry
            //    .Algorithm
            //    .GenerationRan += AlgorithmDebugHelper
            //        .Algorithm_Ran;

            //// when
            //konfiguracjaGry.Algorithm.Start();

            // when
            var result = await konfiguracjaGry.RunGameAsync() as AlgorithmResult;

            // then
            Assert.NotNull(result);
        }
    }
}