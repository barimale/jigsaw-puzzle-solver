using System.Collections.Generic;
using Tangram.Solver.UI.Domain;

namespace Tangram.Solver.UI.Services.Abstractions
{
    public interface IGamePartsDetailsService
    {
        List<GamePartsDetails> GetAll();
        GamePartsDetails GetBy(string id);
    }
}