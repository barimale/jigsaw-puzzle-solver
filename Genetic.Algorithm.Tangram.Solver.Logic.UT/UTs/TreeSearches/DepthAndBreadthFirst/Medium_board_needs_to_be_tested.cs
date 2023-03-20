using Xunit.Abstractions;
using Assert = Xunit.Assert;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.BaseUT;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Helpers;
using Algorithm.Tangram.TreeSearch.Logic;
using Solver.Tangram.Game.Logic;
using Solver.Tangram.AlgorithmDefinitions.AlgorithmsDefinitions;

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
        public async Task Containing_4_blocks_with_X_and_O_markups_and_5x4_board_with_0_and_1_fields()
        {
            // given
            var gameParts = GameConfiguratorBuilder
                .AvalaibleGameSets
                .CreatePolishMediumBoard(withAllowedLocations: true);

            var breadthFirstAlg = new BreadthFirstTreeSearchAlgorithm(gameParts.Board, gameParts.Blocks);
            var depthFirstAlg = new DepthFirstTreeSearchAlgorithm(gameParts.Board, gameParts.Blocks);
            var pilotAlg = new PilotTreeSearchAlgorithm(gameParts.Board, gameParts.Blocks);

            // when
            var pilot = breadthFirstAlg.ExecuteAsync();
            var depthFirst = depthFirstAlg.ExecuteAsync();
            var breadthFirst = pilotAlg.ExecuteAsync();

            var results = await Task.WhenAll(new[] { depthFirst, breadthFirst, pilot });

            // then
            Assert.Equal(3, results.Length);

            // finally
            Display("DepthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(results[0].Solution as FindFittestSolution);

            Display("BreadthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(results[1].Solution as FindFittestSolution);

            Display("Pilot");
            AlgorithmUTConsoleHelper.ShowMadeChoices(results[2].Solution as FindFittestSolution);
        }

        [Fact]
        public async Task Containing_4_blocks_and_5x4_board()
        {
            // given
            var gameParts = GameConfiguratorBuilder
                .AvalaibleGameSets
                .CreateMediumBoard(withAllowedLocations: true);

            var breadthFirstAlg = new BreadthFirstTreeSearchAlgorithm(gameParts.Board, gameParts.Blocks);
            var depthFirstAlg = new DepthFirstTreeSearchAlgorithm(gameParts.Board, gameParts.Blocks);
            var pilotAlg = new PilotTreeSearchAlgorithm(gameParts.Board, gameParts.Blocks);

            // when
            var pilot = breadthFirstAlg.ExecuteAsync();
            var depthFirst = depthFirstAlg.ExecuteAsync();
            var breadthFirst = pilotAlg.ExecuteAsync();

            var results = await Task.WhenAll(new[] { depthFirst, breadthFirst, pilot });

            // then
            Assert.Equal(3, results.Length);

            // finally
            Display("DepthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(results[0].Solution as FindFittestSolution);

            Display("BreadthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(results[1].Solution as FindFittestSolution);

            Display("Pilot");
            AlgorithmUTConsoleHelper.ShowMadeChoices(results[2].Solution as FindFittestSolution);
        }
    }
}