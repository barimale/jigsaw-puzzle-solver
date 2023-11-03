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

        public AlgorithmDetailsService(ILogger<AlgorithmDetailsService> logger)
        {
            _logger = logger;
            //_logger.LogInformation("AlgorithmDetailsService initialized.");
        }

        private List<string> AllAlgorithms = new List<string>()
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
            return AllAlgorithms.Select(p => 
            new AlgorithmDetails()
            {
                Id = Guid.NewGuid().ToString(),
                Name = StringHelper.ToSpaceSeparatedString(p),
                Code = p
            }).ToList();
        }
    }
}
