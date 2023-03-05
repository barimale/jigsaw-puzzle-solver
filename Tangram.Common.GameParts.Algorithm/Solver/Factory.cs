namespace Genetic.Algorithm.Tangram.AlgorithmSettings.Solver
{
    public static class SolverFactory
    {
        public static SolverBuilder CreateNew()
        {
            return new SolverBuilder();
        }
    }
}
