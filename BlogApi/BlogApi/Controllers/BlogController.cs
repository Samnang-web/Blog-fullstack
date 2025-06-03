using BlogApi.DTOs;
using BlogApi.Models;
using BlogApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepo;
        public BlogController(IBlogRepository blogRepo)
        {
            _blogRepo = blogRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var blog = await _blogRepo.GetAllAsync();
            return Ok(blog);
        }

       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var blog = await _blogRepo.GetByIdAsync(id);
            if (blog == null)
                return BadRequest("Not Found!");
            return Ok(blog);
        }

        [Authorize(Roles = "Admin, Author")]
        [HttpPost]
        public async Task<IActionResult> AddNew([FromForm] BlogCreateDto blog)
        {
            try
            {
                if (blog.ImageUrl == null || blog.ImageUrl.Length == 0)
                    return BadRequest("Image file is required.");

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var ext = Path.GetExtension(blog.ImageUrl.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(ext))
                    return BadRequest("Invalid image file type.");

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(blog.ImageUrl.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await blog.ImageUrl.CopyToAsync(stream);
                }

                var imageUrl = "/images/" + fileName;

                var newBlog = new BlogCreateDto
                {
                    Title = blog.Title,
                    Description = blog.Description,
                    Content = blog.Content,
                    AuthorName = blog.AuthorName,
                    CategoryId = blog.CategoryId,
                    ImageUrl = null
                };

                var newId = await _blogRepo.AddAsync(newBlog, imageUrl);
                return CreatedAtAction("GetById", new { id = newId }, new
                {
                    blog.Title,
                    blog.Description,
                    blog.Content,
                    blog.AuthorName,
                    blog.CategoryId,
                    ImageUrl = imageUrl
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server Error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin,Editor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BlogUpdateDto blog)
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            if (roleClaim == null)
                return Forbid("No role claim");

            if (roleClaim != "Admin" && roleClaim != "Editor")
                return Forbid("You are not authorized");

            await _blogRepo.UpdateAsync(id, blog);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogRepo.DeleteAsync(id);
            return NoContent();
        }

    }
}
