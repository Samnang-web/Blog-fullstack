using BlogApi.DTOs;
using BlogApi.Repository;
using BlogApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IUserRepository _userRepo;
        public AuthController(UserService userService, IUserRepository userRepo)
        {
            _userService = userService;
            _userRepo = userRepo;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTo loginDto)
        {
            var user = await _userService.LoginAsync(loginDto.Email, loginDto.Password);

            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });

            // Check if user has admin role
            if (user.Role != "Admin" && user.Role != "Editor" && user.Role != "Author") // adjust if it's a list or different property
                {
                return Unauthorized(new { message = "Access denied. Only admin users can login." });
            }

            return Ok(user);
        }

        [Authorize (Roles = "Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTo dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _userService.RegisterUserAsync(dto);
                return Ok(new { Message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                // You can handle duplicate email or other exceptions here
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
