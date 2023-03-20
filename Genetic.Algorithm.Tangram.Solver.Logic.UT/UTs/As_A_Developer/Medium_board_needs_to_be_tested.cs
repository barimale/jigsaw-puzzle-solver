using Xunit.Abstractions;
using Assert = Xunit.Assert;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.BaseUT;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Helpers;
using Solver.Tangram.Game.Logic;
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

            var game = new GameConfiguratorBuilder()
                .WithGamePartsConfigurator(gameParts)
                .WithAlgorithm(algorithm)
                .Build();

            // when
            var result = await game.RunGameAsync<AlgorithmResult>();

            // then
            Assert.NotNull(result);

            // finally
            Display("GeneticAlgorithm");
            AlgorithmUTConsoleHelper.ShowChromosome(result?.GetSolution<TangramChromosome>());
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

            var game = new GameConfiguratorBuilder()
                .WithGamePartsConfigurator(gameParts)
                .WithAlgorithm(algorithm)
                .Build();

            // when
            var result = await game.RunGameAsync<AlgorithmResult>();

            // then
            Assert.NotNull(result);

            // finally
            Display("GeneticAlgorithm");
            AlgorithmUTConsoleHelper.ShowChromosome(result?.GetSolution<TangramChromosome>());
        }
    }
}