using BookFinderAPI.Data;
using BookFinderAPI.Interfaces;
using BookFinderAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookFinderAPI.Repository
{
    public class GenerateJwtToken : IGenerateJwtToken
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public GenerateJwtToken(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // Generate JWT Token
        public string GenerateJwtTokens(User user)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SigningKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
         new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
         new Claim(ClaimTypes.Name, user.Username)
     };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
