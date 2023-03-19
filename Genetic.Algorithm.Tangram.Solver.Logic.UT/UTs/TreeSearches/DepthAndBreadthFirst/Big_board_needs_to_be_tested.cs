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
        public async Task Containing_10_blocks_and_5x10_board()
        {
            // given
            var gameParts = GamePartConfiguratorBuilder
                .AvalaibleGameSets
                .CreateBigBoard(withAllowedLocations: true);

            // TODO: wrapper over the geneticalgorithm
            // choosing the algorithm depending on the amount of blocks
            // <= 5 GA, more TS or parallel or support for parallel and by default selection
            // depending on the amount of blocks
            // so the calculationStrategy -> single or array, with mode whenall/first
            // at the end add the MCTS from GameAI lib
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
            var pilot = solution.PilotMethodAsync();

            var results = await Task.WhenAll(new[] { 
                depthFirst, breadthFirst, pilot });

            Assert.Equal(3, results.Length);

            // finally
            Display("DepthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(results[0]);

            Display("BreadthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(results[1]);

            Display("Pilot");
            AlgorithmUTConsoleHelper.ShowMadeChoices(results[2]);
        }
    }
}