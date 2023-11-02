using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Christmas.Secret.Gifter.API.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly ILogger<AuthorizeService> _logger;

        public AuthorizeService(IConfiguration configuration, ILogger<AuthorizeService> logger)
        {
            _logger = logger;
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        //public string GetToken(IdentityUser user)
        //{
        //    try
        //    {
        //        var utcNow = DateTime.UtcNow;

        //        var claims = new Claim[]
        //        {
        //                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        //                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
        //                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //                new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
        //        };

        //        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<String>("Tokens:Key")));
        //        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        //        var jwt = new JwtSecurityToken(
        //            signingCredentials: signingCredentials,
        //            claims: claims,
        //            notBefore: utcNow,
        //            expires: utcNow.AddSeconds(Configuration.GetValue<int>("Tokens:Lifetime")),
        //            audience: Configuration.GetValue<string>("Tokens:Audience"),
        //            issuer: Configuration.GetValue<string>("Tokens:Issuer")
        //            );


        //        return new JwtSecurityTokenHandler().WriteToken(jwt);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        return null;
        //    }
        //}

        public string GetToken(IdentityUser user)
        {
            try
            {
                var utcNow = DateTime.UtcNow;

                var claims = new Claim[]
                {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
                };

                //var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<String>("Tokens:Key")));
                //var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
                //var jwt = new JwtSecurityToken(
                //    signingCredentials: signingCredentials,
                //    claims: claims,
                //    notBefore: utcNow,
                //    expires: utcNow.AddSeconds(Configuration.GetValue<int>("Tokens:Lifetime")),
                //    audience: Configuration.GetValue<string>("Tokens:Audience"),
                //    issuer: Configuration.GetValue<string>("Tokens:Issuer")
                //    );

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("Tokens:Key"));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = utcNow.AddSeconds(Configuration.GetValue<int>("Tokens:Lifetime")),
                    NotBefore = utcNow,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);

                //return new JwtSecurityTokenHandler().WriteToken(jwt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
