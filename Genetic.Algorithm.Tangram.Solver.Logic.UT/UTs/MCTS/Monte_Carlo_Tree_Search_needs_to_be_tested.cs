using Algorithm.Tangram.MCTS.Logic;
using Genetic.Algorithm.Tangram.GameParts;
using Genetic.Algorithm.Tangram.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using Genetic.Algorithm.Tangram.Solver.Domain.Generators;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Base;
using TreesearchLib;
using Xunit.Abstractions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.As_A_Developer
{
    public class Monte_Carlo_Tree_Search_needs_to_be_tested : PrintToConsoleUTBase
    {
        public Monte_Carlo_Tree_Search_needs_to_be_tested(ITestOutputHelper output)
            : base(output)
        {
            // intentionally left blank
        }

        [Fact]
        public void For_simple_board_with_two_blocks_only()
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
                preconfiguredBlocks
                );

            var depthFirst = solution.DepthFirst();

            var resultRS100 = Minimize.Start(solution).RakeSearch(100);
            Display($"RakeSearch(100) {resultRS100.BestQuality} {resultRS100.VisitedNodes} ({(resultRS100.VisitedNodes / resultRS100.Elapsed.TotalSeconds):F2} nodes/sec)");

            var resultAnytimeLD = Minimize.Start(solution).AnytimeLDSearch(3);
            Display($"AnytimeLDSearch(3) {resultAnytimeLD.BestQuality} {resultAnytimeLD.VisitedNodes} ({(resultAnytimeLD.VisitedNodes / resultAnytimeLD.Elapsed.TotalSeconds):F2} nodes/sec)");
        }
    }
}