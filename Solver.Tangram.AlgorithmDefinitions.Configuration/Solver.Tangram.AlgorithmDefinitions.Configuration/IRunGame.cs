using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;

namespace Solver.Tangram.Game.Logic
{
    public interface IRunGame: IExecutionEstimationCallback
    {
        public Task<T?> RunGameAsync<T>(CancellationToken ct = default)
            where T : class;
    }
}