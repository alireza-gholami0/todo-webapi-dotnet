using first_project.Models;
using first_project.Models.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace first_project.Services
{
    public class AuthService
    {
        private readonly UserService _userService;
        private readonly PasswordHasher<User> _passwordHasher = new();
        private readonly IConfiguration _configuration;
        public AuthService(UserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        public async Task<string> Login(LoginDTO loginDTO)
        {
            var user = await _userService.GetByEmail(loginDTO.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDTO.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            var token = GenerateToken(user);
            return token;
        }
        public async Task<User> Register(RegisterDTO registerDTO)
        {
            var user = new User
            {
                Name = registerDTO.Name,
                Email = registerDTO.Email,
                Password = registerDTO.Password
            };
            try
            {
                await _userService.CreateUser(user);
                return user;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }
        private string GenerateToken(User user)
        {
            var jwtSetting = _configuration.GetSection("JWT");
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id!),
                new Claim("role", user.Role.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting["key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSetting["Issuer"],
                audience: jwtSetting["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(int.Parse(jwtSetting["ExpiresInDays"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
