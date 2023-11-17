using Microsoft.AspNetCore.Identity;

namespace Tangram.Solver.UI.Services.Abstractions
{
    public interface IAuthorizeService
    {
        string GetToken(IdentityUser user);
    }
}