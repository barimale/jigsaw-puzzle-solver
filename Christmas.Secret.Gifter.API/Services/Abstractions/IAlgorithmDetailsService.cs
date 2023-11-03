using Christmas.Secret.Gifter.Domain;
using System.Collections.Generic;

namespace Christmas.Secret.Gifter.API.Services.Abstractions
{
    public interface IAlgorithmDetailsService
    {
        List<AlgorithmDetails> GetAll();
    }
}