using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System;
using Tangram.Solver.UI.Services.Abstractions;
using Tangram.Solver.UI.Utilities;
using Tangram.Solver.UI.Domain;

namespace Tangram.Solver.UI.Services
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
