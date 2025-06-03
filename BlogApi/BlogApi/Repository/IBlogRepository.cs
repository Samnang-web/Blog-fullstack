using BlogApi.DTOs;
using BlogApi.Models;

namespace BlogApi.Repository
{
    public interface IBlogRepository
    {
        Task<List<BlogDTo>> GetAllAsync();
        Task<Blog> GetByIdAsync(int id);
        Task<int> AddAsync(BlogCreateDto blog, string imageUrl);
        Task UpdateAsync(int id, BlogUpdateDto blog);
        Task DeleteAsync(int id);
    }
}
