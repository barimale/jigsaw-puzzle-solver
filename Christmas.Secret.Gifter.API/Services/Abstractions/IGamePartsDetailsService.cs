using System.Collections.Generic;

namespace Christmas.Secret.Gifter.API.Services.Abstractions
{
    public interface IGamePartsDetailsService
    {
        List<string> GetAll();
    }
}