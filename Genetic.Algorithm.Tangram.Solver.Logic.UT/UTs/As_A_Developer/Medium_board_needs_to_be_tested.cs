using Xunit.Abstractions;
using Assert = Xunit.Assert;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.BaseUT;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Helpers;
using Solver.Tangram.Game.Logic;
using Solver.Tangram.AlgorithmDefinitions.AlgorithmsDefinitions;
using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using Solver.Tangram.AlgorithmDefinitions.Generics;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.UTs.As_A_Developer
{
    public class Medium_board_needs_to_be_tested : PrintToConsoleUTBase
    {
        private AlgorithmUTConsoleHelper AlgorithmUTConsoleHelper;

        public Medium_board_needs_to_be_tested(ITestOutputHelper output)
            : base(output)
        {
            AlgorithmUTConsoleHelper = new AlgorithmUTConsoleHelper(output);
        }

        [Fact]
        public async Task Containing_4_blocks_with_X_and_O_markups_and_5x4_board_with_0_and_1_fields()
        {
            // given
            var gameParts = GameConfiguratorBuilder
                .AvalaibleGameSets
                .CreatePolishMediumBoard(withAllowedLocations: true);

            var algorithm = GameConfiguratorBuilder
                .AvalaibleGATunedAlgorithms
                .CreateMediumBoardSettings(
                    gameParts.Board,
                    gameParts.Blocks,
                    gameParts.AllowedAngles);

            var game = new Game(gameParts, algorithm);

            // when
            var solution = game.RunGameAsync<AlgorithmResult>();

            var results = await Task.WhenAll(new[] { solution });

            // then
            Assert.Equal(1, results.Length);

            // finally
            Display("GeneticAlgorithm");
            AlgorithmUTConsoleHelper.ShowChromosome(results[0].Solution as TangramChromosome);
        }

        [Fact]
        public async Task Containing_4_blocks_and_5x4_board()
        {
            // given
            var gameParts = GameConfiguratorBuilder
                .AvalaibleGameSets
                .CreateMediumBoard(withAllowedLocations: true);

            var algorithm = GameConfiguratorBuilder
                .AvalaibleGATunedAlgorithms
                .CreateMediumBoardSettings(
                    gameParts.Board,
                    gameParts.Blocks,
                    gameParts.AllowedAngles);

            var game = new Game(gameParts, algorithm);

            // when
            var solution = game.RunGameAsync<AlgorithmResult>();

            var results = await Task.WhenAll(new[] { solution });

            // then
            Assert.Equal(1, results.Length);

            // finally
            Display("GeneticAlgorithm");
            AlgorithmUTConsoleHelper.ShowChromosome(results[0].Solution as TangramChromosome);
        }
    }
}