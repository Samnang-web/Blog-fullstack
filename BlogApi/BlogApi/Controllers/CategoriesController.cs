using BlogApi.DTOs;
using BlogApi.Models;
using BlogApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _cateRepo;
        public CategoriesController(ICategoryRepository cateRepo)
        {
            _cateRepo = cateRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cate = await _cateRepo.GetCategoriesAsync();
            return Ok(cate);
        }
       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cate = await _cateRepo.GetById(id);
            return Ok(cate);
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryCreateDto category)
        {
            await _cateRepo.AddAsynce(category);
            return CreatedAtAction("GetById", new {id = category.Id}, category);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryCreateDto category)
        {
            await _cateRepo.UpdateAsynce(id, category);
            return Ok();
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _cateRepo.DeleteAsynceAsync(id);
            return Ok();
        }

    }
}
