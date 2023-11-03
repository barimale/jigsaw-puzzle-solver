using System.Collections.Generic;

namespace Christmas.Secret.Gifter.API.Services.Abstractions
{
    public interface IAlgorithmDetailsService
    {
        List<string> GetAll();
    }
}