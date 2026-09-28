using Algorithm.Tangram.TreeSearch.Logic;
using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.GameParts.Board;

namespace Solver.Tangram.AlgorithmDefinitions.AlgorithmsDefinitions
{
    public class OrToolsSatAlgorithm : Algorithm<FindSATFittestSolution>, IExecutableAlgorithm
    {
        private const string NAME = "OrToolsSatAlgorithm";

        private int maximalAmountOfIterations;

        public OrToolsSatAlgorithm(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int? maxDegreeOfParallelism = null)
            : base(new FindSATFittestSolution(board, blocks))
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

            FindSATFittestSolution? result;

            result = OrToolsHelper.OrToolsHelper.Solve(base.algorithm.Board, base.algorithm.Blocks);

            return new AlgorithmResult()
            {
                Fitness = result != null ? result.Fitness : 100000.ToString(),
                Solution = result,
                IsError = !result.HasError
            };
        }
    }
}
