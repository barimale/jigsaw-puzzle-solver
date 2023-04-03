namespace Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm
{
    public interface IExecutableAlgorithm: IQualityCallback, IExecutionEstimationCallback
    {
        public Task<AlgorithmResult> ExecuteAsync(CancellationToken ct = default);
        public string Name { get; }
        public string Id { get; }
    }
}