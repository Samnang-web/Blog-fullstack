using BlogApi.DTOs;
using BlogApi.Models;

namespace BlogApi.Repository
{
    public interface IBlogRepository
    {
        Task<IEnumerable<BlogDto>> GetAllAsync();
        Task<Blog?> GetByIdAsync(int id);
        Task<int> AddAsync(BlogCreateDto blog, string imageUrl);
        Task<bool> UpdateAsync(int id, BlogUpdateDto blog);
        Task<bool> DeleteAsync(int id);
    }
}
