using BlogApi.DTOs;
using BlogApi.Models;

namespace BlogApi.Services
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogDTo?>> GetAllBlog();
        Task<Blog?> GetByIdAsync(int id);
        Task<int> CreateAsync(BlogCreateDto dto, string imageUrl);
        Task<bool> UpdateAsync(int id, BlogUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
