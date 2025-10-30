using ReEV.Service.Auth.DTOs;
using System.Security.Claims;

namespace ReEV.Service.Auth.Services.Interfaces
{
    public interface ITokenService
    {
        Task<TokenResponseDTO> GenerateTokensAsync(UserDTO user);
        Task<TokenResponseDTO> RenewToken(string accessToken, string refreshToken);
        string GenerateAccessToken(UserDTO user);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
