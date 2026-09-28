using Algorithm.Tangram.TreeSearch.Logic;
using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.GameParts.Board;
using TreesearchLib;

namespace Solver.Tangram.AlgorithmDefinitions.AlgorithmsDefinitions
{
    public class OrToolsSatAlgorithm : Algorithm<FindFittestSolution>, IExecutableAlgorithm
    {
        private const string NAME = "OrToolsSatAlgorithm";

        private int maximalAmountOfIterations;

        public OrToolsSatAlgorithm(
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

            FindFittestSolution? result = new FindFittestSolution(base.algorithm.Board, base.algorithm.Blocks.ToList());

            var solvedResult = OrToolsHelper.OrToolsHelper.Solve(base.algorithm.Board.Height, base.algorithm.Board.Width, base.algorithm.Blocks);

            return new AlgorithmResult()
            {
                Fitness = result != null && result.Quality.HasValue ? result.Quality.Value.ToString() : string.Empty,
                Solution = result,
                IsError = !result.Quality.HasValue
            };
        }
    }
}
