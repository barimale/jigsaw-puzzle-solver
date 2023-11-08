using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Christmas.Secret.Gifter.API.Services.Abstractions;
using Christmas.Secret.Gifter.Domain;
using System.Linq;
using System;

namespace Christmas.Secret.Gifter.API.Services
{
    public class GamePartsDetailsService : IGamePartsDetailsService
    {
        private readonly ILogger<GamePartsDetailsService> _logger;
        private readonly List<GamePartsDetails> AllGameParts;
        private GamePartsDetailsService()
        {
            AllGameParts = AllGamePartsNames.Select(p =>
           new GamePartsDetails()
           {
               Id = Guid.NewGuid().ToString(),
               Name = StringHelper.ToSpaceSeparatedString(p),
               Code = p
           }).ToList();
        }

        public GamePartsDetailsService(ILogger<GamePartsDetailsService> logger)
            : this()
        {
            _logger = logger;
        }

        private List<string> AllGamePartsNames = new List<string>()
        {
            "CreateBigBoard",
            "CreatePolishBigBoard",
            "CreateMediumBoard",
            "CreatePolishMediumBoard",
            "CreateSimpleBoard"
        };

        public GamePartsDetails GetBy(string id)
        {
            return AllGameParts.FirstOrDefault(p => p.Id == id);
        }

        public List<GamePartsDetails> GetAll()
        {
            return AllGameParts;
        }
    }
}
