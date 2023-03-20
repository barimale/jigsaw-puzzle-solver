using Algorithm.Tangram.TreeSearch.Logic;
using Generic.Algorithm.Tangram.GameParts;
using Generic.Algorithm.Tangram.GameParts.Elements.Blocks;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using Genetic.Algorithm.Tangram.Solver.Domain.Generators;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.BaseUT;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Helpers;
using TreesearchLib;
using Xunit.Abstractions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.UTs.TreeSearches.DepthAndBreadthFirst
{
    public class Small_board_needs_to_be_tested : PrintToConsoleUTBase
    {
        private AlgorithmUTConsoleHelper AlgorithmUTConsoleHelper;

        public Small_board_needs_to_be_tested(ITestOutputHelper output)
            : base(output)
        {
            AlgorithmUTConsoleHelper = new AlgorithmUTConsoleHelper(output);
        }

        [Fact]
        public void Containing_2_blocks_and_3x2_board()
        {
            // given
            int scaleFactor = 1;

            IList<BlockBase> blocks = new List<BlockBase>()
            {
                Purple.Create(),
                Black.Create(),
            };

            int[] angles = new int[]
            {
                0,
                90,
                180,
                270
            };

            var fieldHeight = 1d;
            var fieldWidth = 1d;
            var boardColumnsCount = 3;
            var boardRowsCount = 2;
            var fields = GameSetFactory
                .GeneratorFactory
                .RectangularGameFieldsGenerator
                .GenerateFields(
                    scaleFactor,
                    fieldHeight,
                    fieldWidth,
                    boardColumnsCount,
                    boardRowsCount);

            var boardDefinition = new BoardShapeBase(
                fields,
                boardColumnsCount,
                boardRowsCount,
                scaleFactor);

            var modificator = new AllowedLocationsGenerator();
            var preconfiguredBlocks = modificator.Preconfigure(
                    blocks.ToList(),
                    boardDefinition,
                    angles);

            // when
            var solution = new FindFittestSolution(
                boardDefinition,
                preconfiguredBlocks);

            var depthFirst = solution.DepthFirst();
            var breadthFirst = solution.BreadthFirst();

            // then
            Display("DepthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(depthFirst);

            Display("BreadthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(breadthFirst);
        }
    }
}