using CodeFirstSwaggerImpl.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstSwaggerImpl.Services
{
    public interface ITokenService
    {
        Task<ClaimsPrincipal> GetByValue(string value);
        Task<LoginResult> CreateToken(LoginRequest Login);
    }
}
