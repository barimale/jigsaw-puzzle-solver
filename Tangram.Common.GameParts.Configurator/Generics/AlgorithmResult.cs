namespace Solver.Tangram.AlgorithmDefinitions.Generics
{
    public class AlgorithmResult
    {
        public string Fitness { get; set; }
        public object Solution { get; set; } // TODO refactor public T Solution<T>()
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
