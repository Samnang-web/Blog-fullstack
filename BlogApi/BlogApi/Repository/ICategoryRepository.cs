using BlogApi.DTOs;
using BlogApi.Models;

namespace BlogApi.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> GetById(int id);
        Task AddAsynce (CategoryCreateDto category);
        Task UpdateAsynce (int id, CategoryCreateDto category);
        Task DeleteAsynceAsync (int id);
    }
}
