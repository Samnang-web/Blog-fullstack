using BlogApi.DTOs;
using BlogApi.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace BlogApi.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly IConfiguration _conn;
        public BlogRepository(IConfiguration conn)
        {
            _conn = conn;
        }

        public async Task<IEnumerable<BlogDTo?>> GetAllAsync()
        {
            using var connect = GetConnection();

            var sql = @"
                SELECT b.Id, b.Title, b.Description, b.ImageUrl, b.CreatedAt, b.UpdatedAt, b.AuthorName, c.Name AS CategoryName 
                FROM Blogs b 
                INNER JOIN Categories c ON b.CategoryId = c.Id";

            var blogs = await connect.QueryAsync<Blog>(sql);

            // Manually map Blog to BlogDto (or use AutoMapper in service)
            var result = blogs.Select(b => new BlogDTo
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                ImageUrl = b.ImageUrl,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt,
                AuthorName = b.AuthorName,
                CategoryName = "" // You must add logic here if you want to fill CategoryName, or query it separately
            });

            return result;
        }

        public async Task<Blog?> GetByIdAsync(int id)
        {
            using var connect = GetConnection();
            var blog = await connect.QueryFirstOrDefaultAsync<Blog>("SELECT * FROM Blogs WHERE Id=@Id", new { id });
            return blog;
        }

        public async Task<int> AddAsync(BlogCreateDto blog, string imageUrl)
        {
            using var connect = GetConnection();

            var query = @"
        INSERT INTO Blogs (Title, Description, Content, ImageUrl, AuthorName, CategoryId, CreatedAt)
        VALUES (@Title, @Description, @Content, @ImageUrl, @AuthorName, @CategoryId, NOW()) RETURNING Id;";

            var newId = await connect.ExecuteScalarAsync<int>(query, new
            {
                blog.Title,
                blog.Description,
                blog.Content,
                ImageUrl = imageUrl,
                blog.AuthorName,
                blog.CategoryId
            });

            return newId;
        }

        public async Task<bool> UpdateAsync(int id, BlogUpdateDto blog)
        {
            using var connect = GetConnection();
            var rows = await connect.ExecuteAsync(@"
        UPDATE Blogs SET Title = @Title, Description = @Description, Content = @Content, 
        ImageUrl = @ImageUrl, AuthorName = @AuthorName, CategoryId = @CategoryId, UpdatedAt = NOW() 
        WHERE Id = @Id;",
                new
                {
                    blog.Title,
                    blog.Description,
                    blog.Content,
                    blog.ImageUrl,
                    blog.AuthorName,
                    blog.CategoryId,
                    Id = id
                });

            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connect = GetConnection();
            var rows = await connect.ExecuteAsync("DELETE FROM Blogs WHERE Id=@Id", new { Id = id });
            return rows > 0;
        }
        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_conn.GetConnectionString("DBConnection"));
        }
    }
}
