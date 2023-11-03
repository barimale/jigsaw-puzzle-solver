using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Christmas.Secret.Gifter.API.Services.Abstractions;

namespace Christmas.Secret.Gifter.API.Services
{
    public class GamePartsDetailsService : IGamePartsDetailsService
    {
        private readonly ILogger<GamePartsDetailsService> _logger;

        public GamePartsDetailsService(ILogger<GamePartsDetailsService> logger)
        {
            _logger = logger;
            //_logger.LogInformation("AlgorithmDetailsService initialized.");
        }

        private List<string> AllGameParts = new List<string>()
        {
            "CreateBigBoard",
            "CreatePolishBigBoard",
            "CreateMediumBoard",
            "CreatePolishMediumBoard",
            "CreateSimpleBoard"
        };

        //private List<object> AllFactories = new List<object>()
        //{
        //    new GameSetFactory(),
        //};

        public List<string> GetAll()
        {
            return AllGameParts;
        }
    }
}
