using Algorithm.Tangram.MCTS.Logic;
using Genetic.Algorithm.Tangram.Configurator;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Base;
using TreesearchLib;
using Xunit.Abstractions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.As_A_Developer
{
    public class MCTS_needs_to_be_tested : PrintToConsoleUTBase
    {
        public MCTS_needs_to_be_tested(ITestOutputHelper output)
            : base(output)
        {
            // intentionally left blank
        }

        [Fact]
        public void For_medium_board()
        {
            // given
            var gameParts = GamePartConfiguratorBuilder
                .AvalaibleGameSets
                .CreateMediumBoard(withAllowedLocations: true);

            var algorithm = GamePartConfiguratorBuilder
                .AvalaibleTunedAlgorithms
                .CreateMediumBoardSettings(
                    gameParts.Board,
                    gameParts.Blocks,
                    gameParts.AllowedAngles);

            // when
            int size = gameParts.Blocks.Count;

            // then
            var solution = new FindFittestSolution(
                size,
                gameParts.Board,
                gameParts.Blocks);

            var depthFirst = solution.DepthFirst();
            var breadthFirst = solution.BreadthFirst();

            //var mcts = solution.MCTS(
            //    runtime: TimeSpan.FromSeconds(20),
            //    nodelimit: 20);

            //var resultRS = Minimize.Start(solution).RakeSearch(preconfiguredBlocks.Count);
            //Display($"RakeSearch(100) {resultRS.BestQuality} {resultRS.VisitedNodes} ({(resultRS.VisitedNodes / resultRS.Elapsed.TotalSeconds):F2} nodes/sec)");
        }
    }
}