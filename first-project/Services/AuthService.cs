using first_project.Models;
using Microsoft.AspNetCore.Identity;

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
        //public Task<> Login() { }
        //public Task<> Register() { }
        //private srting GenerateToken() { }



}
