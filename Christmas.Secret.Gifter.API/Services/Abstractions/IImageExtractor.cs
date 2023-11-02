using System.Threading.Tasks;

namespace Christmas.Secret.Gifter.API.Services
{
    public interface IImageExtractor
    {
        Task SaveLocallyAsync();
    }
}