using Demo.Utilities;
using Experimental.MVVM.WPF.Solver.Presenter.Utilities.Base;

namespace Experimental.MVVM.WPF.Solver.Presenter.Utilities
{
    public class DisplayerMultiton: Multiton<string, AlgorithmDisplayHelper>
    {
        public DisplayerMultiton()
        {
            // intentionally left blank
        }
    }
}
