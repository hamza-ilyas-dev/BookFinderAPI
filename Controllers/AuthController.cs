using Microsoft.AspNetCore.Mvc;
using BookFinderAPI.Data;
using BookFinderAPI.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BookFinderAPI.Dtos;
using BookFinderAPI.Interfaces;

namespace BookFinderAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly IGenerateJwtToken _generateJwtToken;

        public AuthController(AppDbContext context, IConfiguration config, IGenerateJwtToken generateJwtToken)
        {
            _context = context;
            _config = config;
            _generateJwtToken = generateJwtToken;   
        }

        // User Registration
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDto model)
        {
            if (_context.Users.Any(u => u.Username == model.Username))
            {
                return BadRequest("Username already exists.");
            }

            var user = new User
            {
                Username = model.Username,
                PasswordHash = HashPassword(model.Password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("User registered successfully.");
        }

        // User Login
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == model.Username);

            if (user == null || !VerifyPassword(model.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid username or password.");
            }

            string token = _generateJwtToken.GenerateJwtTokens(user);
            return Ok(new { Token = token });
        }

        // Hash password using SHA256
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        // Verify password hash
        private bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }
         
    }
     
}
