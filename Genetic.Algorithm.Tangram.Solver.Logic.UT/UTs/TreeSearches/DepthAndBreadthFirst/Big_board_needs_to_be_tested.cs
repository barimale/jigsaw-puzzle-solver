using Algorithm.Tangram.MCTS.Logic;
using Genetic.Algorithm.Tangram.Configurator;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Base;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities;
using TreesearchLib;
using Xunit.Abstractions;
using Assert = Xunit.Assert;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.UTs.TreeSearches.DepthAndBreadthFirst
{
    public class Big_board_needs_to_be_tested : PrintToConsoleUTBase
    {
        private AlgorithmUTConsoleHelper AlgorithmUTConsoleHelper;

        public Big_board_needs_to_be_tested(ITestOutputHelper output)
            : base(output)
        {
            AlgorithmUTConsoleHelper = new AlgorithmUTConsoleHelper(output);
        }

        [Fact]
        public async Task With_ten_blocks()
        {
            // given
            var gameParts = GamePartConfiguratorBuilder
                .AvalaibleGameSets
                .CreateBigBoard(withAllowedLocations: true);

            var algorithm = GamePartConfiguratorBuilder
                .AvalaibleTunedAlgorithms
                .CreateBigBoardSettings(
                    gameParts.Board,
                    gameParts.Blocks,
                    gameParts.AllowedAngles);

            // when
            int size = gameParts.Blocks.Count;

            // then
            var solution = new FindFittestSolution(
                size,
                gameParts.Board,
                gameParts.Blocks);

            var depthFirst = solution.DepthFirstAsync();
            var breadthFirst = solution.BreadthFirstAsync();

            var results = await Task.WhenAll(new[] { depthFirst, breadthFirst });

            Assert.Equal(2, results.Length);

            // finally
            Display("DepthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(results[0]);

            Display("BreadthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(results[1]);
        }
    }
}