﻿namespace Solver.Tangram.Configurator.Generics
{
    public interface IExecutableMultiAlgorithm
    {
        public Task<IList<AlgorithmResult>> ExecuteManyAsync(CancellationToken ct = default);
    }
}
