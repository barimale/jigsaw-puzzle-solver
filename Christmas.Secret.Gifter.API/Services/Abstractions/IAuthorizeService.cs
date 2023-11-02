using Microsoft.AspNetCore.Identity;

namespace Christmas.Secret.Gifter.API.Services
{
    public interface IAuthorizeService
    {
        string GetToken(IdentityUser user);
    }
}