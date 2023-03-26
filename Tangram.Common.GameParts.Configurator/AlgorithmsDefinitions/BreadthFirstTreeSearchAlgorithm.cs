using Algorithm.Tangram.TreeSearch.Logic;
using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;
using Solver.Tangram.AlgorithmDefinitions.Generics.Statistics;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.GameParts.Board;
using TreesearchLib;

namespace Solver.Tangram.AlgorithmDefinitions.AlgorithmsDefinitions
{
    public class BreadthFirstTreeSearchAlgorithm : Algorithm<FindFittestSolution>, IExecutableAlgorithm
    {
        private int maximalAmountOfIterations;

        public BreadthFirstTreeSearchAlgorithm(
            BoardShapeBase board,
            IList<BlockBase> blocks)
            : base(new FindFittestSolution(board, blocks))
        {
            this.maximalAmountOfIterations = blocks
                .Select(p => p.AllowedLocations.Length)
                .Aggregate(1, (x, y) => x * y);
        }

        public override async Task<AlgorithmResult> ExecuteAsync(CancellationToken ct = default)
        {
            var result = await algorithm.ParallelBreadthFirstAsync(
                token: ct,
                callback: (state, control, quality) => {
                    base.HandleQualityCallback(state);
                    base.CurrentIteration = state.VisitedNodes;
                    base.HandleExecutionEstimationCallback(state, maximalAmountOfIterations);
                });

            return new AlgorithmResult()
            {
                Fitness = result.Quality.HasValue ? result.Quality.Value.ToString() : string.Empty,
                Solution = result,
                IsError = !result.Quality.HasValue
            };
        }
    }
}
