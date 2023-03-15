using Algorithm.Tangram.MCTS.Logic;
using Genetic.Algorithm.Tangram.GameParts;
using Genetic.Algorithm.Tangram.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using Genetic.Algorithm.Tangram.Solver.Domain.Generators;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Base;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities;
using TreesearchLib;
using Xunit.Abstractions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.UTs.TreeSearches.DepthAndBreadthFirst
{
    public class Dummy_board_needs_to_be_tested : PrintToConsoleUTBase
    {
        private AlgorithmUTConsoleHelper AlgorithmUTConsoleHelper;

        public Dummy_board_needs_to_be_tested(ITestOutputHelper output)
            : base(output)
        {
            AlgorithmUTConsoleHelper = new AlgorithmUTConsoleHelper(output);
        }

        [Fact]
        public void With_two_blocks()
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
                //-270,
                //-180,
                //-90,
                0,
                90,
                180,
                270
            };

            var fieldHeight = 1d;
            var fieldWidth = 1d;
            var boardColumnsCount = 3;
            var boardRowsCount = 2;
            var fields = GamePartsFactory
                .GeneratorsFactory
                .FieldsGenerator
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
            int size = preconfiguredBlocks.Count;

            // then
            var solution = new FindFittestSolution(
                size,
                boardDefinition,
                preconfiguredBlocks);

            var depthFirst = solution.DepthFirst();
            var breadthFirst = solution.BreadthFirst();

            // finally
            Display("DepthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(depthFirst);

            Display("BreadthFirst");
            AlgorithmUTConsoleHelper.ShowMadeChoices(breadthFirst);
        }
    }
}