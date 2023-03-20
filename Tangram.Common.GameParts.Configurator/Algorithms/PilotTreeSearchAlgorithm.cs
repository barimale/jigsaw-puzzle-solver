using Algorithm.Tangram.TreeSearch.Logic;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using Solver.Tangram.Configurator.Generics;
using Solver.Tangram.Configurator.Generics.SingleAlgorithm;
using TreesearchLib;

namespace Solver.Tangram.Configurator.Algorithms
{
    public class PilotTreeSearchAlgorithm : Algorithm<FindFittestSolution>, IExecutableAlgorithm
    {
        public PilotTreeSearchAlgorithm(
            BoardShapeBase board,
            IList<BlockBase> blocks)
            : base(new FindFittestSolution(board, blocks))
        {
            // intentionally left blank
        }

        public override async Task<AlgorithmResult> ExecuteAsync(CancellationToken ct = default)
        {
            var result = await algorithm.PilotMethodAsync(token: ct);

            return new AlgorithmResult()
            {
                Fitness = result.Quality.HasValue ? result.Quality.Value.ToString() : string.Empty,
                Solution = result.Solution,
                IsError = !result.Quality.HasValue
            };
        }
    }
}
