using System.Threading.Tasks;

namespace Christmas.Secret.Gifter.API.HostedServices.Hub
{
    public interface ILocalesStatusHub
    {
        Task OnStartAsync(string id);
        Task OnFinishAsync(string id);
    }
}