using Genetic.Algorithm.Tangram.Solver.Logic.Solver;

namespace Genetic.Algorithm.Tangram.Solver.Logic
{
    public static class SolverFactory
    {
        public static SolverBuilder CreateNew()
        {
            return new SolverBuilder();
        }
    }
}
