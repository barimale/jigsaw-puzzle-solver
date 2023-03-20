namespace Solver.Tangram.Configurator.Generics.SingleAlgorithm
{
    public abstract class Algorithm<T> : IExecutableAlgorithm
        where T : class
    {
        protected T algorithm;

        public Algorithm(T algorithm)
        {
            this.algorithm = algorithm;
        }

        public abstract Task<AlgorithmResult> ExecuteAsync(CancellationToken ct = default);
    }
}
