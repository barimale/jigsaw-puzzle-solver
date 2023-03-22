namespace Solver.Tangram.AlgorithmDefinitions.Generics
{
    // TODO implement the event handler
    public interface IExecutableMultiAlgorithm
    {
        //public event EventHandler QualityCallback;
        public Task<AlgorithmResult[]> ExecuteManyAsync(CancellationToken ct = default);
    }
}
