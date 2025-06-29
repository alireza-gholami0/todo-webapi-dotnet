using first_project.Models;
using first_project.Models.DTOs.Auth;
using first_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace first_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var token = await _authService.Login(loginDTO);
                return Ok(token);
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                var user = await _authService.Register(registerDTO);
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
