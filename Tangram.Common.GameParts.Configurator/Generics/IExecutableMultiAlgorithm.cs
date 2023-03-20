namespace Solver.Tangram.AlgorithmDefinitions.Generics
{
    public interface IExecutableMultiAlgorithm
    {
        public Task<AlgorithmResult[]> ExecuteManyAsync(CancellationToken ct = default);
    }
}
