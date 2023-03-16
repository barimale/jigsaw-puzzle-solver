using Algorithm.Tangram.MCTS.Logic;
using Genetic.Algorithm.Tangram.Configurator;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Base;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities;
using TreesearchLib;
using Xunit.Abstractions;
using Assert = Xunit.Assert;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.UTs.TreeSearches.DepthAndBreadthFirst
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
        public async Task With_5_blocks()
        {
            // given
            var gameParts = GamePartConfiguratorBuilder
                .AvalaibleGameSets
                .CreateMediumBoard(withAllowedLocations: true);

            var algorithm = GamePartConfiguratorBuilder
                .AvalaibleTunedAlgorithms
                .CreateMediumBoardSettings(
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

            var depthFirst = solution.DepthFirst(); // Async
            var breadthFirst = solution.BreadthFirst(); //Async

            //var results = await Task.WhenAll(new[] { depthFirst, breadthFirst });

            //Assert.Equal(2, results.Length);

            // finally
            Display("DepthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(depthFirst);

            Display("BreadthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(breadthFirst);
        }
    }
}