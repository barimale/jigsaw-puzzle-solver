namespace Solver.Tangram.Configurator.Generics.SingleAlgorithm
{
    public interface IExecutableAlgorithm
    {
        public Task<AlgorithmResult> ExecuteAsync(CancellationToken ct = default);
    }
}
