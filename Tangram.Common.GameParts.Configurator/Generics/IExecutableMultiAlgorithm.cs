namespace Solver.Tangram.AlgorithmDefinitions.Generics
{
    public interface IExecutableMultiAlgorithm
    {
        public Task<IList<AlgorithmResult>> ExecuteManyAsync(CancellationToken ct = default);
    }
}
