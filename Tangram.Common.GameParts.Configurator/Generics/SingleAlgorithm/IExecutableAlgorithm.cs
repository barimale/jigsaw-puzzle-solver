namespace Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm
{
    public interface IExecutableAlgorithm
    {
        public event EventHandler QualityCallback;
        public Task<AlgorithmResult> ExecuteAsync(CancellationToken ct = default);
    }
}
