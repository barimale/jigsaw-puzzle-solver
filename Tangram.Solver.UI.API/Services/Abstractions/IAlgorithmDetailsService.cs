using System.Collections.Generic;
using Tangram.Solver.UI.Domain;

namespace Tangram.Solver.UI.Services.Abstractions
{
    public interface IAlgorithmDetailsService
    {
        List<AlgorithmDetails> GetAll();
        AlgorithmDetails GetBy(string id);
    }
}