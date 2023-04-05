using Algorithm.Tangram.TreeSearch.Logic;
using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.GameParts.Board;
using TreesearchLib;

namespace Solver.Tangram.AlgorithmDefinitions.AlgorithmsDefinitions
{
    public class BreadthFirstTreeSearchAlgorithm : Algorithm<FindFittestSolution>, IExecutableAlgorithm
    {
        private const string NAME = "BreadthFirstTreeSearchAlgorithm";

        private int maximalAmountOfIterations;

        public BreadthFirstTreeSearchAlgorithm(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int? maxDegreeOfParallelism = null)
            : base(new FindFittestSolution(board, blocks))
        {
            this.maximalAmountOfIterations = blocks
                .Select(p => p.AllowedLocations.Length)
                .Aggregate(1, (x, y) => x * y);
            base.maxDegreeOfParallelism = maxDegreeOfParallelism;
        }

        public override string Name => NAME;

        public override async Task<AlgorithmResult> ExecuteAsync(CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();

            FindFittestSolution? result;
            switch(algorithm.Blocks.Count)
            {
                case int n when n < 3:
                    result = algorithm.BreadthFirst(
                        token: ct,
                        callback: (state, control, quality) => {
                            base.HandleQualityCallback(state);
                            base.CurrentIteration = state.VisitedNodes;
                            base.HandleExecutionEstimationCallback(state, maximalAmountOfIterations);
                        });
                    break;
                case int n when n < 5:
                        result = await algorithm.BreadthFirstAsync(
                        token: ct,
                        callback: (state, control, quality) => {
                            base.HandleQualityCallback(state);
                            base.CurrentIteration = state.VisitedNodes;
                            base.HandleExecutionEstimationCallback(state, maximalAmountOfIterations);
                        });
                    break;
                default:
                    result = await algorithm.ParallelBreadthFirstAsync(
                        token: ct,
                        callback: (state, control, quality) => {
                            base.HandleQualityCallback(state);
                            base.CurrentIteration = state.VisitedNodes;
                            base.HandleExecutionEstimationCallback(state, maximalAmountOfIterations);
                        });
                    break;
            }

            return new AlgorithmResult()
            {
                Fitness = result.Quality.HasValue ? result.Quality.Value.ToString() : string.Empty,
                Solution = result,
                IsError = !result.Quality.HasValue
            };
        }
    }
}
