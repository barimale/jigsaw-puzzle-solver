using Xunit.Abstractions;
using Assert = Xunit.Assert;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.BaseUT;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Helpers;
using Algorithm.Tangram.TreeSearch.Logic;
using Solver.Tangram.Game.Logic;
using Solver.Tangram.AlgorithmDefinitions.AlgorithmsDefinitions;
using Solver.Tangram.AlgorithmDefinitions.Generics;

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
            var gameParts = GameConfiguratorBuilder
                .AvalaibleGameSets
                .CreateBigBoard(withAllowedLocations: true);

            var breadthFirstAlg = GameConfiguratorBuilder
                .AvalaibleTSTemplatesAlgorithms
                .CreateBreadthFirstTreeSearchAlgorithm(
                    gameParts.Board,
                    gameParts.Blocks);

            var depthFirstAlg = GameConfiguratorBuilder
                .AvalaibleTSTemplatesAlgorithms
                .CreateDepthFirstTreeSearchAlgorithm(
                    gameParts.Board,
                    gameParts.Blocks);

            var pilotAlg = GameConfiguratorBuilder
                .AvalaibleTSTemplatesAlgorithms
                .CreatePilotTreeSearchAlgorithm(
                    gameParts.Board,
                    gameParts.Blocks);

            var game = new GameConfiguratorBuilder()
                .WithGamePartsConfigurator(gameParts)
                .WithManyAlgorithms()
                .WithExecutionMode(ExecutionMode.WhenAll)
                .WithAlgorithms(depthFirstAlg,
                        breadthFirstAlg,
                        pilotAlg)
                .Build();

            // when
            var results = await game.RunGameAsync<IList<AlgorithmResult>>();
            var resultsAsArray = results.ToArray();

            // then
            Assert.NotNull(resultsAsArray);
            Assert.Equal(3, resultsAsArray.Length);

            // finally
            Display("DepthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(resultsAsArray[0].Solution as FindFittestSolution);

            Display("BreadthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(resultsAsArray[1].Solution as FindFittestSolution);

            Display("Pilot");
            AlgorithmUTConsoleHelper.ShowMadeChoices(resultsAsArray[2].Solution as FindFittestSolution);
        }


        [Fact]
        public async Task Containing_10_blocks_with_X_and_O_markups_and_5x10_board_with_0_and_1_fields()
        {
            // given
            var gameParts = GameConfiguratorBuilder
                .AvalaibleGameSets
                .CreatePolishBigBoard(withAllowedLocations: true);

            var breadthFirstAlg = GameConfiguratorBuilder
                .AvalaibleTSTemplatesAlgorithms
                .CreateBreadthFirstTreeSearchAlgorithm(
                    gameParts.Board,
                    gameParts.Blocks);

            var depthFirstAlg = GameConfiguratorBuilder
                .AvalaibleTSTemplatesAlgorithms
                .CreateDepthFirstTreeSearchAlgorithm(
                    gameParts.Board,
                    gameParts.Blocks);

            var pilotAlg = GameConfiguratorBuilder
                .AvalaibleTSTemplatesAlgorithms
                .CreatePilotTreeSearchAlgorithm(
                    gameParts.Board,
                    gameParts.Blocks);

            var game = new GameConfiguratorBuilder()
                .WithGamePartsConfigurator(gameParts)
                .WithManyAlgorithms()
                .WithExecutionMode(ExecutionMode.WhenAll)
                .WithAlgorithms(depthFirstAlg,
                        breadthFirstAlg,
                        pilotAlg)
                .Build();

            // when
            var results = await game.RunGameAsync<IList<AlgorithmResult>>();
            var resultsAsArray = results.ToArray();

            // then
            Assert.NotNull(resultsAsArray);
            Assert.Equal(3, resultsAsArray.Length);

            // finally
            Display("DepthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(resultsAsArray[0].Solution as FindFittestSolution);

            Display("BreadthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(resultsAsArray[1].Solution as FindFittestSolution);

            Display("Pilot");
            AlgorithmUTConsoleHelper.ShowMadeChoices(resultsAsArray[2].Solution as FindFittestSolution);
        }
    }
}