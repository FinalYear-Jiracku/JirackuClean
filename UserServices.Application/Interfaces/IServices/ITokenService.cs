using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UserServices.Application.Interfaces.IServices
{
    public interface ITokenService
    {
        string GenerateRefreshToken();
        string GenerateAccessToken(IEnumerable<Claim> claims);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
