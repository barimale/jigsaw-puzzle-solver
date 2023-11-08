using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Christmas.Secret.Gifter.API.Services.Abstractions;
using Christmas.Secret.Gifter.Domain;
using System;
using System.Linq;

namespace Christmas.Secret.Gifter.API.Services
{
    public class AlgorithmDetailsService : IAlgorithmDetailsService
    {
        private readonly ILogger<AlgorithmDetailsService> _logger;
        private readonly List<AlgorithmDetails> AllAlgorithms;

        private AlgorithmDetailsService()
        {
            AllAlgorithms = AllAlgorithmsCodes
                .Select(p =>
                    new AlgorithmDetails()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = StringHelper.ToSpaceSeparatedString(p),
                        Code = p
                    })
                .ToList();
        }

        public AlgorithmDetailsService(ILogger<AlgorithmDetailsService> logger)
            : this()
        {
            _logger = logger;
        }

        private List<string> AllAlgorithmsCodes = new List<string>()
        {
            "BreadthFirstTreeSearchAlgorithm",
            "DepthFirstTreeSearchAlgorithm",
            "BinaryDepthFirstTreeSearchAlgorithm",
            "OneRootParallelDepthFirstTreeSearchAlgorithm",
            "BinaryOneRootParallelDepthFirstTreeSearchAlgorithm",
            "PilotTreeSearchAlgorithm",
            "ExecutableGeneticAlgorithm",
            "ExecutableVaryRatiosGeneticAlgorithm"
        };

        //private List<object> AllFactories = new List<object>()
        //{
        //    new TSTemplatesFactory(),
        //    new GATemplatesFactory(),
        //    new GAVaryRatiosTemplatesFactory()
        //};

        public List<AlgorithmDetails> GetAll()
        {
            return AllAlgorithms;
        }

        public AlgorithmDetails GetBy(string id)
        {
            return GetAll().AsQueryable().FirstOrDefault(p => p.Id == id);
        }
    }
}
