using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zaku.Domain.Entities;
using Zaku.Domain.Interfaces;

namespace Zaku.Infrastructure.Security
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfigurationSection _jwtSettings;
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("Jwt");
        }

        public string CreateToken(User user)
        {
            string secretKey = _configuration["Jwt:Secret"]!;

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email)
                    //new Claim(JwtRegisteredClaimNames.EmailVerified, user.EmailVerified.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"] ?? "60")),
                SigningCredentials = signingCredentials,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            string token = handler.CreateEncodedJwt(tokenDescriptor);
        
            return token;
        }

        public JwtSecurityToken GenerateToken(User user)
        {
            JwtSettings jwtSettingsObj = new JwtSettings
            {
                Issuer = _jwtSettings["Issuer"] ?? string.Empty,
                Audience = _jwtSettings["Audience"] ?? string.Empty,
                SecretKey = _configuration["Jwt:Secret"] ?? string.Empty,
                ExpiryMinutes = int.TryParse(_jwtSettings["ExpiryMinutes"], out var m) ? m : 0
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettingsObj.SecretKey));

            Claim[] claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.UserType.ToString())
            };

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: jwtSettingsObj.Issuer,
                audience: jwtSettingsObj.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettingsObj.ExpiryMinutes),
                signingCredentials: creds);

            return token;
        }
    }
}
