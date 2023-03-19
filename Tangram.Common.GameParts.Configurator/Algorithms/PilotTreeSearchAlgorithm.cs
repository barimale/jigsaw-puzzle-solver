using Algorithm.Tangram.MCTS.Logic;
using Genetic.Algorithm.Tangram.Configurator.Generics;
using Genetic.Algorithm.Tangram.Configurator.Generics.SingleAlgorithm;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using TreesearchLib;

namespace Genetic.Algorithm.Tangram.Configurator.Algorithms
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
