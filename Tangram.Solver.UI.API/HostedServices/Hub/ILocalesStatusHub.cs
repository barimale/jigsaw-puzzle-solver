using System.Threading.Tasks;

namespace Tangram.Solver.UI.HostedServices.Hub
{
    public interface ILocalesStatusHub
    {
        Task OnStartAsync(string id);
        Task OnFinishAsync(string id);
        Task OnProgressAsync(string finess);
        Task OnNewResultFoundAsync(string input);
    }
}