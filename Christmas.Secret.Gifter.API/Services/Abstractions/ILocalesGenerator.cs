using System.Threading.Tasks;

namespace Christmas.Secret.Gifter.API.Services
{
    public interface ILocalesGenerator
    {
        Task GenerateAsync();
    }
}