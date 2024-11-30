using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MVC.Services
{
    public class JwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string korisnickoIme, string uloga)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            // Ključevi i konfiguracija
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Klijmovi (podaci u tokenu)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, korisnickoIme),
                new Claim(ClaimTypes.Role, uloga),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Stvaranje tokena
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            // Vraćanje tokena kao string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}