using BlogApi.Repository;
using BlogApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            var user = await _userRepo.GetAllUser();
            return Ok(user);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.Identity?.Name;

            if (email == null)
                return Unauthorized(new { message = "Invalid token or user not authenticated." });

            var user = await _userRepo.GetByEmailAsynce(email); // You'll need to implement this in your repo
            if (user == null)
                return NotFound(new { message = "User not found." });

            return Ok(user);
        }
        [HttpDelete("{id}")]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userRepo.DeleteUser(id);
            return Ok();
        }
    }
}
