using Genetic.Algorithm.Tangram.Solver.Logic.Helpers;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.BaseUT;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Helpers;
using Solver.Tangram.AlgorithmDefinitions;
using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.Game.Logic;
using Tangram.GameParts.Logic.GameParts;
using Xunit.Abstractions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.UTs.As_A_Developer
{
    public class I_would_like_to_solve_simple_board : PrintToConsoleUTBase
    {
        private AlgorithmDebugHelper AlgorithmDebugHelper;
        private AlgorithmUTConsoleHelper AlgorithmUTConsoleHelper;

        public I_would_like_to_solve_simple_board(ITestOutputHelper output)
            : base(output)
        {
            AlgorithmDebugHelper = new AlgorithmDebugHelper();
            AlgorithmUTConsoleHelper = new AlgorithmUTConsoleHelper(output);
        }

        [Fact]
        public async Task With_shape_of_2x2_fields_by_using_3_blocks_and_no_unused_fields()
        {
            // given
            var gameParts = GameConfiguratorBuilder
                .AvalaibleGameSets
                .CreateSimpleBoard(withAllowedLocations: true);

            var algorithm = GameConfiguratorBuilder
                .AvalaibleGATunedAlgorithms
                .CreateSimpleBoardSettings(
                    gameParts.Board,
                    gameParts.Blocks,
                    gameParts.AllowedAngles);

            var konfiguracjaGry = new GameConfiguratorBuilder()
                .WithAlgorithm(algorithm)
                .WithGamePartsConfigurator(gameParts)
                .Build();

            if (konfiguracjaGry.Algorithm == null)
                return;

            // settings
            var showAllowedLocations = true;
            var multicolorSupported = false;

            // display board
            Display("Board definition:");
            base.Display(konfiguracjaGry.GameSet.BoardAsStringArray());

            // display blocks
            Display("Block definitions:");
            foreach (var block in konfiguracjaGry.GameSet.Blocks)
            {
                // display definition
                base.Display(block.BlockAsStringArray());

                if (showAllowedLocations)
                {
                    Display(string.Empty);
                    Display("Block locations START:");
                    foreach (var location in block.AllowedLocations)
                    {
                        // display allowed locations
                        base.Display("Block " + block.Color.ToString() + "definition(allOfThem:" + block.AllowedLocations.Length + ")");
                        Display(GameSet.LocationAsStringArray(location));
                    }
                    Display("Block locations END.");
                    Display(string.Empty);
                }
            }

            //konfiguracjaGry.Algorithm.TerminationReached += AlgorithmDebugHelper.Algorithm_TerminationReached;
            //konfiguracjaGry.Algorithm.TerminationReached += AlgorithmUTConsoleHelper.Algorithm_TerminationReached;

            //konfiguracjaGry.Algorithm.GenerationRan += AlgorithmDebugHelper.Algorithm_Ran;
            //konfiguracjaGry.Algorithm.GenerationRan += AlgorithmUTConsoleHelper.Algorithm_Ran;

            //// when
            //konfiguracjaGry.Algorithm.Start();
            // when
            var result = await konfiguracjaGry.RunGameAsync() as AlgorithmResult;

            // then
            Assert.NotNull(result);
        }
    }
}