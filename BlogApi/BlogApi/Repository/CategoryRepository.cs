using BlogApi.DTOs;
using BlogApi.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BlogApi.Repository
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly IConfiguration _conn;
        public CategoryRepository(IConfiguration conn)
        {
            _conn = conn;
        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            using var connect = GetConnection();
            var query = await connect.QueryAsync<Category>("SELECT * FROM Categories");
            return query.ToList();
        }

        public async Task<Category> GetById(int id)
        {
            using var connect = GetConnection();
            var query = await connect.QueryFirstOrDefaultAsync<Category>("SELECT * FROM Categories WHERE Id = @Id", new { Id = id });
            return query;
        }

        public async Task AddAsynce(CategoryCreateDto category)
        {
            using var connect = GetConnection();
            var query = "INSERT INTO Categories (Name) VALUES (@Name); SELECT CAST(SCOPE_IDENTITY() AS int);";
            var newId = await connect.ExecuteScalarAsync<int>(query, category);
            category.Id = newId;
        }

        public async Task UpdateAsynce(int id, CategoryCreateDto category)
        {
            using var connect = GetConnection();
            await connect.ExecuteAsync("UPDATE Categories SET Name=@Name WHERE Id = @Id",new {category.Name, Id=id});
        }

        public async Task DeleteAsynceAsync(int id)
        {
            using var connect = GetConnection();
            await connect.ExecuteAsync("DELETE FROM Categories WHERE Id=@Id", new { Id = id });
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_conn.GetConnectionString("DBConnection"));
        }

        
    }
}
