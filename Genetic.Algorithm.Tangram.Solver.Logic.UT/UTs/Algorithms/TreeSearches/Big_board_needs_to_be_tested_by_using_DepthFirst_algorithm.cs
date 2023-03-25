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
    public class Big_board_needs_to_be_tested_by_using_DepthFirst_algorithm : PrintToConsoleUTBase
    {
        private AlgorithmUTConsoleHelper AlgorithmUTConsoleHelper;

        public Big_board_needs_to_be_tested_by_using_DepthFirst_algorithm(ITestOutputHelper output)
            : base(output)
        {
            AlgorithmUTConsoleHelper = new AlgorithmUTConsoleHelper(output);
        }

        [Fact]
        public async Task Containing_10_blocks_and_5x10_board()
        {
            // given
            var gameParts = GameBuilder
                .AvalaibleGameSets
                .CreateBigBoard(withAllowedLocations: true);

            var depthFirstAlg = GameBuilder
                .AvalaibleTSTemplatesAlgorithms
                .CreateDepthFirstTreeSearchAlgorithm(
                    gameParts.Board,
                    gameParts.Blocks);

            var game = new GameBuilder()
                .WithGamePartsConfigurator(gameParts)
                .WithAlgorithm(depthFirstAlg)
                .Build();

            // when
            var result = await game.RunGameAsync<AlgorithmResult>();

            // then
            Assert.NotNull(result);

            // finally
            Display("DepthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(result.GetSolution<FindFittestSolution>());
        }

        [Fact(Timeout=1000*60*60*48)] // 48hours timeout
        public async Task Containing_10_blocks_with_X_and_O_markups_and_5x10_board_with_0_and_1_fields()
        {
            // given
            var gameParts = GameBuilder
                .AvalaibleGameSets
                .CreatePolishBigBoard(withAllowedLocations: true);

                // TODO WIP refactor as a wrapper for GameBuilder
                // reorder gameparts
                var orderedBlocks = gameParts
                    .Blocks
                    .OrderByDescending(p => p.AllowedLocations.Length)
                    .ToList();

                gameParts.Blocks.Clear();
                orderedBlocks.ForEach(pp => gameParts.Blocks.Add(pp));

            var depthFirstAlg = GameBuilder
                 .AvalaibleTSTemplatesAlgorithms
                 .CreateOneRootParallelDepthFirstTreeSearchAlgorithm(
                     gameParts.Board,
                     gameParts.Blocks);

            var game = new GameBuilder()
                .WithGamePartsConfigurator(gameParts)
                .WithAlgorithm(depthFirstAlg)
                .Build();

            game.OnExecutionEstimationReady += Algorithm_OnExecutionEstimationReady;

            // when
            var result = await game.RunGameAsync<AlgorithmResult>();

            // then
            Assert.NotNull(result);

            // finally
            Display("DepthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(result.GetSolution<FindFittestSolution>());
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
    }
}