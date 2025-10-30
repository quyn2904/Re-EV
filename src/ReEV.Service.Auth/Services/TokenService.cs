using Microsoft.IdentityModel.Tokens;
using ReEV.Service.Auth.DTOs;
using ReEV.Service.Auth.Models;
using ReEV.Service.Auth.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ReEV.Service.Auth.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;
        private readonly IRefreshTokenRepository _refreshTokenRepo;

        public TokenService(IConfiguration config, IUserRepository userRepo, IRefreshTokenRepository refreshTokenRepo)
        {
            _config = config;
            _refreshTokenRepo = refreshTokenRepo;
        }

        public async Task<TokenResponseDTO> GenerateTokensAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var accessExpiry = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:AccessTokenExpiryMinutes"]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = accessExpiry,
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);
            var jwtId = securityToken.Id;
            var refreshToken = new RefreshToken
            {
                Token = GenerateRandomTokenString(),
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(Convert.ToDouble(_config["Jwt:RefreshTokenExpiryDays"])),
            };
            await _refreshTokenRepo.CreateAsync(refreshToken);

            return new TokenResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                AccessTokenExpiresAt = accessExpiry
            };
        }

        private string GenerateRandomTokenString()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
