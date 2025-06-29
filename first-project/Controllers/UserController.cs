using first_project.Models;
using first_project.Models.DTOs.User;
using first_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace first_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _userService.GetAllUsers();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser([FromRoute] string id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] User user)
        {
            try
            {
                await _userService.CreateUser(user);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}/name")]
        public async Task<ActionResult<User>> PutUser(
            [FromRoute] string id,
            [FromBody] EditUserNameDto editUserNameDto
            )
        {
            try
            {
                await _userService.UpdateUserName(id, editUserNameDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser([FromRoute] string id)
        {
            var user = await _userService.DeleteUser(id);
            
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
