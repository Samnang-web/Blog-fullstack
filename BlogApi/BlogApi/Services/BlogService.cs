using BlogApi.DTOs;
using BlogApi.Models;
using BlogApi.Repository;

namespace BlogApi.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repo;

        public BlogService(IBlogRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<BlogDTo?>> GetAllBlog()
        {
            return await _repo.GetAllAsync();
        }

        public Task<Blog?> GetByIdAsync(int id)
        {
            return _repo.GetByIdAsync(id);
        }

        public Task<int> CreateAsync(BlogCreateDto dto, string imageUrl)
        {
            return _repo.AddAsync(dto, imageUrl);
        }

        public Task<bool> UpdateAsync(int id, BlogUpdateDto dto)
        {
            return _repo.UpdateAsync(id, dto);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _repo.DeleteAsync(id);
        }

    }
}
