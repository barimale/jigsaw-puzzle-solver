using Solver.Tangram.AlgorithmDefinitions.Generics;
using System.Threading.Tasks;
using Tangram.Solver.UI.Utilities;

namespace Christmas.Secret.Gifter.API.HostedServices.Hub
{
    public interface ILocalesStatusHub
    {
        Task OnStartAsync(string id);
        Task OnFinishAsync(string id);
        Task OnProgressAsync(string finess);
        Task OnNewResultFoundAsync(PolygonPairsResult input);
    }
}