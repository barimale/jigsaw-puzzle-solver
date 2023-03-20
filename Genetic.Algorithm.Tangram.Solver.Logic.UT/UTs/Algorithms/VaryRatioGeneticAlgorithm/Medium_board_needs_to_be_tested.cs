using Xunit.Abstractions;
using Assert = Xunit.Assert;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.BaseUT;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Helpers;
using Solver.Tangram.Game.Logic;
using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using Solver.Tangram.AlgorithmDefinitions.Generics;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.UTs.Algorithms.VaryRatioGeneticAlgorithm
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
            var maximalGenerationsAmount = 10000;

            var gameParts = GameBuilder
                .AvalaibleGameSets
                .CreatePolishMediumBoard(withAllowedLocations: true);

            var algorithm = GameBuilder
                .AvalaibleGAVaryRatiosTunedAlgorithms
                .CreateMediumBoardSettings(
                    gameParts.Board,
                    gameParts.Blocks,
                    gameParts.AllowedAngles,
                    maximalGenerationsAmount);

            var game = new GameBuilder()
                .WithGamePartsConfigurator(gameParts)
                .WithAlgorithm(algorithm)
                .Build();

            // when
            var result = await game.RunGameAsync<AlgorithmResult>();

            // then
            Assert.NotNull(result);

            // finally
            Display("VaryRatioGeneticAlgorithm");
            AlgorithmUTConsoleHelper.ShowChromosome(result?.GetSolution<TangramChromosome>());
        }

        [Fact]
        public async Task Containing_4_blocks_and_5x4_board()
        {
            // given
            var maximalGenerationsAmount = 10000;

            var gameParts = GameBuilder
                .AvalaibleGameSets
                .CreateMediumBoard(withAllowedLocations: true);

            var algorithm = GameBuilder
                .AvalaibleGAVaryRatiosTunedAlgorithms
                .CreateMediumBoardSettings(
                    gameParts.Board,
                    gameParts.Blocks,
                    gameParts.AllowedAngles,
                    maximalGenerationsAmount);

            var game = new GameBuilder()
                .WithGamePartsConfigurator(gameParts)
                .WithAlgorithm(algorithm)
                .Build();

            // when
            var result = await game.RunGameAsync<AlgorithmResult>();

            // then
            Assert.NotNull(result);

            // finally
            Display("VaryRatioGeneticAlgorithm");
            AlgorithmUTConsoleHelper.ShowChromosome(result?.GetSolution<TangramChromosome>());
        }
    }
}