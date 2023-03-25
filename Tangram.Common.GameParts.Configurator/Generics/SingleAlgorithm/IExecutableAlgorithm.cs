namespace Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm
{
    public interface IExecutableAlgorithm: IQualityCallback, IExecutionEstimationCallback
    {
        public Task<AlgorithmResult> ExecuteAsync(CancellationToken ct = default);
    }
}