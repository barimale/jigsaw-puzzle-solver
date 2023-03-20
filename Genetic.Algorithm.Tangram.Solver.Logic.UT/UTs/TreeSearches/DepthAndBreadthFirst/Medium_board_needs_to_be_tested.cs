using TreesearchLib;
using Xunit.Abstractions;
using Assert = Xunit.Assert;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.BaseUT;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Helpers;
using Algorithm.Tangram.TreeSearch.Logic;
using Tangram.GameParts.Elements.Elements.Blocks;
using Tangram.GameParts.Elements.Elements.Blocks.CommonSettings;
using Tangram.GameParts.Elements;
using Tangram.GameParts.Logic.Generators;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.GameParts.Board;
using Solver.Tangram.Game.Logic;

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
            int scaleFactor = 1;
            var fieldHeight = 1d;
            var fieldWidth = 1d;

            IList<BlockBase> blocks = new List<BlockBase>()
            {
                Purple.Create(withFieldRestrictions: true),
                DarkBlue.Create(withFieldRestrictions: true),
                LightBlue.Create(withFieldRestrictions: true),
                Blue.Create(withFieldRestrictions: true),
            };

            int[] angles = new int[]
            {
                0,
                90,
                180,
                270
            };

            var boardColumnsCount = 5;
            var boardRowsCount = 4;
            var fields = GameSetFactory
                .GeneratorFactory
                .RectangularGameFieldsGenerator
                .GenerateFields(
                    scaleFactor,
                    fieldHeight,
                    fieldWidth,
                    boardColumnsCount,
                    boardRowsCount,
                    new object[,] {
                        { 1, 0, 1, 0, 1},
                        { 0, 1, 0, 1, 0},
                        { 1, 0, 1, 0, 1},
                        { 0, 1, 0, 1, 0},
                    }
                );

            var boardDefinition = new BoardShapeBase(
                fields,
                boardColumnsCount,
                boardRowsCount,
                scaleFactor);

            // leftTuple - blockMarkup
            // rightTuple - boardMarkup
            var allowedMatches = new List<Tuple<string, int>>()
            {
                Tuple.Create("O", 1),
                Tuple.Create("X", 0)
            };

            var modificator = new AllowedLocationsGenerator(
                allowedMatches,
                fieldHeight,
                fieldWidth,
                new List<object>() { PolishGameBaseBlock.SkippedMarkup }
            );

            var preconfiguredBlocks = modificator.Preconfigure(
                    blocks.ToList(),
                    boardDefinition,
                    angles);

            // when
            int size = preconfiguredBlocks.Count;

            // then
            var solution = new FindFittestSolution(
                boardDefinition,
                preconfiguredBlocks);

            var pilot = solution.PilotMethodAsync();
            var depthFirst = solution.DepthFirstAsync();
            var breadthFirst = solution.BreadthFirstAsync();

            var results = await Task.WhenAll(new[] { depthFirst, breadthFirst, pilot });

            Assert.Equal(3, results.Length);

            // finally
            Display("DepthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(results[0]);

            Display("BreadthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(results[1]);

            Display("Pilot");
            AlgorithmUTConsoleHelper.ShowMadeChoices(results[2]);
        }

        [Fact]
        public async Task Containing_4_blocks_and_5x4_board()
        {
            // given
            var gameParts = GameConfiguratorBuilder
                .AvalaibleGameSets
                .CreateMediumBoard(withAllowedLocations: true);

            var algorithm = GameConfiguratorBuilder
                .AvalaibleGATunedAlgorithms
                .CreateMediumBoardSettings(
                    gameParts.Board,
                    gameParts.Blocks,
                    gameParts.AllowedAngles);

            var solution = new FindFittestSolution(
                gameParts.Board,
                gameParts.Blocks);

            // when
            var pilot = solution.PilotMethodAsync();
            var depthFirst = solution.DepthFirstAsync();
            var breadthFirst = solution.BreadthFirstAsync();

            // then
            var results = await Task.WhenAll(new[] { depthFirst, breadthFirst, pilot });

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