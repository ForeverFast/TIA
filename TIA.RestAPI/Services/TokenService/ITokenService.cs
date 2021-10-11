using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TIA.RestAPI.Services
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(string username, string password);
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        bool IsTokenValid(string token);
        Task<ClaimsIdentity> GetIdentity(string username, string password);
    }
}
