using CodeFirstSwaggerImpl.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstSwaggerImpl.Services
{
    public class TokenIdentityValidationParameters : TokenValidationParameters
    {
        public TokenIdentityValidationParameters(string Issuer, string Audience, SymmetricSecurityKey SecurityKey)
        {
            RequireSignedTokens = true;
            ValidAudience = Audience;
            ValidateAudience = true;
            ValidIssuer = Issuer;
            ValidateIssuer = true;
            ValidateIssuerSigningKey = true;
            ValidateLifetime = true;
            IssuerSigningKey = SecurityKey;
            AuthenticationType = "Bearer";
        }
    }

    public class TokenService : ITokenService
    {
        private ILogger Logger { get; }
        private string Issuer { get; }
        private string Audience { get; }
        private TimeSpan ValidityDuration { get; }

        private SigningCredentials Credentials { get; }
        private TokenIdentityValidationParameters ValidationParameters { get; }

        public TokenService(IConfiguration Configuration, ILogger<TokenService> Logger, IServiceProvider serviceProvider)
        {
            this.Logger = Logger;

            IConfigurationSection JWTConfiguration = serviceProvider.GetRequiredService<IConfiguration>().GetSection("JWT");
            Issuer = JWTConfiguration["Issuer"] ?? "MyApiProducer";
            Audience = JWTConfiguration["Audience"] ?? "MyApiConsumer";
            ValidityDuration = TimeSpan.FromDays(1);// Todo: JWTConfiguration["ValidityDuration"];
            string Key = JWTConfiguration["Key"] ?? "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890";

            SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));

            Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);

            ValidationParameters = new TokenIdentityValidationParameters(Issuer, Audience, SecurityKey);
        }

        public async Task<ClaimsPrincipal> GetByValue(string Value)
        {
            if (Value == null)
            {
                throw new Exception("No Token supplied");
            }

            JwtSecurityTokenHandler Handler = new JwtSecurityTokenHandler();

            try
            {
                SecurityToken ValidatedToken;
                ClaimsPrincipal Principal = Handler.ValidateToken(Value, ValidationParameters, out ValidatedToken);

                return await Task.FromResult(Principal);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<LoginResult> CreateToken(LoginRequest Login)
        {
            JwtSecurityToken Token = await CreateToken(new Claim[] {
                new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.Name, Login.Email)
            });

            return new LoginResult(Token);
        }

        private async Task<JwtSecurityToken> CreateToken(Claim[] Claims)
        {
            JwtHeader Header = new JwtHeader(Credentials);

            JwtPayload Payload = new JwtPayload(Issuer,
                           Audience,
                                                Claims,
                                                DateTime.UtcNow,
                                                DateTime.UtcNow.Add(ValidityDuration),
                                                DateTime.UtcNow);

            JwtSecurityToken SecurityToken = new JwtSecurityToken(Header, Payload);

            return await Task.FromResult(SecurityToken);
        }

    }
}
