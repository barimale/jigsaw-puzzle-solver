using Algorithm.Tangram.TreeSearch.Logic;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.BaseUT;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Helpers;
using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.AlgorithmDefinitions.OrToolsHelper;
using Solver.Tangram.Game.Logic;
using Xunit.Abstractions;
using Assert = Xunit.Assert;

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
        public async Task Containing_4_blocks_with_X_and_O_markups_and_5x4_board_with_0_and_1_fields_with_ortools_sat()
        {
            // given
            var gameParts = GameBuilder
                .AvalaibleGameSets
                .CreatePolishBigBoard(withAllowedLocations: true);

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

            // when
            // zbudowac wrappery , wydzileic algorytm 
            //var results = await game.RunGameAsync<AlgorithmResult[]>();
            var results2 = OrToolsHelper.Solve(gameParts.Board.Height, gameParts.Board.Width, gameParts.Blocks);
            //List<IndexedBlockBase> mappedResults = results2.Select(x => new IndexedBlockBase
            //{
            //     BlockDefinition = gameParts.Blocks.FirstOrDefault(xx=> xx.ID == results2[xx.ID.ToString()]),
                  

            //})
            //var resultsTransformed = results
            //    .Select(p => p.GetSolution<FindFittestSolution>())
            //    .ToArray();

            //// then
            //Assert.NotNull(results);
            //Assert.Equal(2, results.Length);

            //// finally
            //Display("DepthFirst");
            //AlgorithmUTConsoleHelper.ShowMadeChoices(resultsTransformed[0]);

            //Display("Pilot");
            //AlgorithmUTConsoleHelper.ShowMadeChoices(resultsTransformed[1]);
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

            // when
            var results = await game.RunGameAsync<AlgorithmResult[]>();
            var resultsAsArray = results?.ToArray();

            // then
            Assert.NotNull(resultsAsArray);
            Assert.Equal(3, resultsAsArray?.Length);

            // finally
            Display("DepthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(resultsAsArray[0]?.GetSolution<FindFittestSolution>());

            Display("BreadthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(resultsAsArray[1]?.GetSolution<FindFittestSolution>());

            Display("Pilot");
            AlgorithmUTConsoleHelper.ShowMadeChoices(resultsAsArray[2]?.GetSolution<FindFittestSolution>());
        }
    }
}