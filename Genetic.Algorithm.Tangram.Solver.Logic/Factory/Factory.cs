namespace Genetic.Algorithm.Tangram.Solver.Logic.Factory
{
    public static class Factory
    {
        public static SolverBuilder CreateNew()
        {
            return new SolverBuilder();
        }
    }
}
