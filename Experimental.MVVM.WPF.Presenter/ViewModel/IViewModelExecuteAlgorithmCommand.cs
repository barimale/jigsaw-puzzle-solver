using System.Windows.Input;

namespace Demo.ViewModel
{
    public interface IViewModelExecuteAlgorithmCommand
    {
        public ICommand ExecuteCommand { get; set; }
    }
}
