using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using TreesearchLib;
using Xunit.Abstractions;
using Assert = Xunit.Assert;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.BaseUT;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Helpers;
using Genetic.Algorithm.Tangram.Solver.Domain.Generators;
using Generic.Algorithm.Tangram.GameParts.Elements.Blocks;
using Generic.Algorithm.Tangram.GameParts.Elements.Blocks.CommonSettings;
using Solver.Tangram.Configurator;
using Generic.Algorithm.Tangram.GameParts;
using Algorithm.Tangram.TreeSearch.Logic;

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

            var algorithm = GameConfiguratorBuilder
                .AvalaibleGATunedAlgorithms
                .CreateBigBoardSettings(
                    gameParts.Board,
                    gameParts.Blocks,
                    gameParts.AllowedAngles);

            // when

            var solution = new FindFittestSolution(
                gameParts.Board,
                gameParts.Blocks);

            var depthFirst = solution.DepthFirstAsync();
            var breadthFirst = solution.BreadthFirstAsync();
            var pilot = solution.PilotMethodAsync();

            var results = await Task.WhenAll(new[] { 
                depthFirst, breadthFirst, pilot });

            // then
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
        public async Task Containing_10_blocks_with_X_and_O_markups_and_5x10_board_with_0_and_1_fields()
        {
            // given
            int scaleFactor = 1;
            var fieldHeight = 1d;
            var fieldWidth = 1d;

            IList<BlockBase> blocks = new List<BlockBase>()
            {
                DarkBlue.Create(withFieldRestrictions: true),
                Red.Create(withFieldRestrictions: true),
                LightBlue.Create(withFieldRestrictions: true),
                Purple.Create(withFieldRestrictions: true),
                Blue.Create(withFieldRestrictions: true),
                Pink.Create(withFieldRestrictions: true),
                Green.Create(withFieldRestrictions: true),
                LightGreen.Create(withFieldRestrictions: true),
                Orange.Create(withFieldRestrictions: true),
                Yellow.Create(withFieldRestrictions: true)
            };

            int[] angles = new int[]
            {
                0,
                90,
                180,
                270
            };

            var boardColumnsCount = 10;
            var boardRowsCount = 5;
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
                        { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 },
                        { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
                        { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 },
                        { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
                        { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 }
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
            var solution = new FindFittestSolution(
                boardDefinition,
                preconfiguredBlocks);

            var pilot = solution.PilotMethodAsync();
            var depthFirst = solution.DepthFirstAsync();
            var breadthFirst = solution.BreadthFirstAsync();

            var results = await Task.WhenAll(new[] { depthFirst, breadthFirst, pilot });

            // then
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