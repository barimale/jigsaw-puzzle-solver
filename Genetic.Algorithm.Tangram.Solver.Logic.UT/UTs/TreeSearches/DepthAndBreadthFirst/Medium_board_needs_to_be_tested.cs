using Xunit.Abstractions;
using Assert = Xunit.Assert;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.BaseUT;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Helpers;
using Algorithm.Tangram.TreeSearch.Logic;
using Solver.Tangram.Game.Logic;
using Solver.Tangram.AlgorithmDefinitions.Generics;

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
            var results = await game.RunGameAsync<AlgorithmResult[]>();

            var resultsTransformed = results
                .Select(p => p.GetSolution<FindFittestSolution>())
                .ToArray();

            // then
            Assert.NotNull(results);
            Assert.Equal(3, results.Length);

            // finally
            Display("DepthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(resultsTransformed[0]);

            Display("BreadthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(resultsTransformed[1]);

            Display("Pilot");
            AlgorithmUTConsoleHelper.ShowMadeChoices(resultsTransformed[2]);
        }

        [Fact]
        public async Task Containing_4_blocks_and_5x4_board()
        {
            // given
            var gameParts = GameConfiguratorBuilder
                .AvalaibleGameSets
                .CreateMediumBoard(withAllowedLocations: true);

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
            var results = await game.RunGameAsync<AlgorithmResult[]>();
            var resultsAsArray = results?.ToArray();

            // then
            Assert.NotNull(resultsAsArray);
            Assert.Equal(3, resultsAsArray?.Length);

            // finally
            var depthFirst = resultsAsArray[0].GetSolution<FindFittestSolution>();

            Display("DepthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(resultsAsArray[0]?.GetSolution<FindFittestSolution>());

            Display("BreadthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(resultsAsArray[1]?.GetSolution<FindFittestSolution>());

            Display("Pilot");
            AlgorithmUTConsoleHelper.ShowMadeChoices(resultsAsArray[2]?.GetSolution<FindFittestSolution>());
        }
    }
}