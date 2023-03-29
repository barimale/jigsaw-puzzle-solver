using Xunit.Abstractions;
using Assert = Xunit.Assert;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.BaseUT;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Helpers;
using Algorithm.Tangram.TreeSearch.Logic;
using Solver.Tangram.Game.Logic;
using Solver.Tangram.AlgorithmDefinitions.Generics;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.UTs.Algorithms.TreeSearches.CheckIfOrderOfBlocksDescreaseTheExecutionTime
{
    public class ParallelSingleRootDepthFirst_for_big_board_needs_to_be_tested : PrintToConsoleUTBase
    {
        private AlgorithmUTConsoleHelper AlgorithmUTConsoleHelper;

        public ParallelSingleRootDepthFirst_for_big_board_needs_to_be_tested(ITestOutputHelper output)
            : base(output)
        {
            AlgorithmUTConsoleHelper = new AlgorithmUTConsoleHelper(output);
        }

        [Fact]
        public async Task Containing_10_blocks_with_X_and_O_markups_and_10x5_board_with_0_and_1_fields()
        {
            // given
            var gameParts = GameBuilder
                .AvalaibleGameSets
                .CreatePolishBigBoard(withAllowedLocations: true);

            var algorithm = GameBuilder
                .AvalaibleTSTemplatesAlgorithms
                .CreateOneRootParallelDepthFirstTreeSearchAlgorithm(
                    gameParts.Board,
                    gameParts.Blocks);

            var game = new GameBuilder()
                .WithGamePartsConfigurator(gameParts)
                .WithAlgorithm(algorithm)
                .Build();

            // when
            var result = await game.RunGameAsync<AlgorithmResult>();

            // then
            Assert.NotNull(result);

            // finally
            Display("DepthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(result.GetSolution<FindFittestSolution>());
        }
    }
}