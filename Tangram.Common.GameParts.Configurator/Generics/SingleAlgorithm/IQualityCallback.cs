using Solver.Tangram.AlgorithmDefinitions.Generics.EventArgs;

namespace Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm
{
    public interface IQualityCallback
    {
        public event EventHandler<SourceEventArgs> QualityCallback;
    }
}