using Solver.Tangram.Game.Logic;

namespace Demo.ViewModel
{
    public interface IViewModelGameInstance
    {
        Game Game { get; protected set; }
    }
}
