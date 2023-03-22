namespace Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm
{
    public interface IExecutableAlgorithm: IQualityCallback
    {
        public Task<AlgorithmResult> ExecuteAsync(CancellationToken ct = default);
    }
}