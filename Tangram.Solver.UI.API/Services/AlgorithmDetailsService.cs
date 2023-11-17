using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using System.Linq;
using Tangram.Solver.UI.Services.Abstractions;
using Tangram.Solver.UI.Utilities;
using Tangram.Solver.UI.Domain;

namespace Tangram.Solver.UI.Services
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
