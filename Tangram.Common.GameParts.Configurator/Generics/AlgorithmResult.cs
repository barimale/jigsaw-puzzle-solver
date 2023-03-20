namespace Solver.Tangram.AlgorithmDefinitions.Generics
{
    public class AlgorithmResult
    {
        public string Fitness { get; set; }
        public object Solution { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }

        public T GetSolution<T>()
        {
            return (T)Solution;
        }
    }
}
