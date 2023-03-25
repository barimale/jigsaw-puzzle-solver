using Xunit.Abstractions;
using Assert = Xunit.Assert;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.BaseUT;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Helpers;
using Algorithm.Tangram.TreeSearch.Logic;
using Solver.Tangram.Game.Logic;
using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.AlgorithmDefinitions.Generics.Statistics;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.UTs.Algorithms.TreeSearches
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
            var gameParts = GameBuilder
                .AvalaibleGameSets
                .CreatePolishMediumBoard(withAllowedLocations: true);

            var depthFirstAlg = GameBuilder
                .AvalaibleTSTemplatesAlgorithms
                .CreateDepthFirstTreeSearchAlgorithm(
                    gameParts.Board,
                    gameParts.Blocks);

            var pilotAlg = GameBuilder
                .AvalaibleTSTemplatesAlgorithms
                .CreatePilotTreeSearchAlgorithm(
                    gameParts.Board,
                    gameParts.Blocks);

            var game = new GameBuilder()
                .WithGamePartsConfigurator(gameParts)
                .WithManyAlgorithms()
                .WithExecutionMode(ExecutionMode.WhenAll)
                .WithAlgorithms(
                    depthFirstAlg,
                    pilotAlg)
                .Build();

            game.OnExecutionEstimationReady += Algorithm_OnExecutionEstimationReady;

            // when
            var results = await game.RunGameAsync<AlgorithmResult[]>();

            var resultsTransformed = results
                .Select(p => p.GetSolution<FindFittestSolution>())
                .ToArray();

            // then
            Assert.NotNull(results);
            Assert.Equal(2, results.Length);

            // finally
            Display("DepthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(resultsTransformed[0]);

            Display("Pilot");
            AlgorithmUTConsoleHelper.ShowMadeChoices(resultsTransformed[1]);
        }

        private void Algorithm_OnExecutionEstimationReady(object? sender, EventArgs e)
        {
            var result = sender as StatisticDetails;

            if (result == null)
                return;

            Display("Pilot-AlgorithmId:");
            Display(result.AlgorithmId);

            Display("Pilot-MeanExecutionTimeOfIterationInSeconds:");
            Display((result.MeanExecutionTimeOfIterationInMiliseconds / 1000).ToString());

            Display("Pilot-EstimatedExecutionTimeInMiliseconds:");
            Display((result.EstimatedExecutionTimeInMiliseconds / 1000).ToString());
        }

        [Fact]
        public async Task Containing_4_blocks_and_5x4_board()
        {
            // given
            var gameParts = GameBuilder
                .AvalaibleGameSets
                .CreateMediumBoard(withAllowedLocations: true);

            var breadthFirstAlg = GameBuilder
                .AvalaibleTSTemplatesAlgorithms
                .CreateBreadthFirstTreeSearchAlgorithm(
                    gameParts.Board,
                    gameParts.Blocks);

            var depthFirstAlg = GameBuilder
                .AvalaibleTSTemplatesAlgorithms
                .CreateDepthFirstTreeSearchAlgorithm(
                    gameParts.Board,
                    gameParts.Blocks);

            var pilotAlg = GameBuilder
                .AvalaibleTSTemplatesAlgorithms
                .CreatePilotTreeSearchAlgorithm(
                    gameParts.Board,
                    gameParts.Blocks);

            var game = new GameBuilder()
                .WithGamePartsConfigurator(gameParts)
                .WithManyAlgorithms()
                .WithExecutionMode(ExecutionMode.WhenAll)
                .WithAlgorithms(depthFirstAlg,
                        breadthFirstAlg,
                        pilotAlg)
                .Build();

            game.Algorithm.OnExecutionEstimationReady += Algorithm_OnExecutionEstimationReady;

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