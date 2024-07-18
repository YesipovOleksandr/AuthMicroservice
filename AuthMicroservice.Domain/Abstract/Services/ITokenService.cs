using AuthMicroservice.Domain.Models;
using System.Security.Claims;

namespace AuthMicroservice.Domain.Abstract.Services
{
    public interface ITokenService
    {
        string GenerateAnonymousAccessToken();
        string GenerateAccessToken(User model);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
