using BlogApi.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace BlogApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _conn;
        public UserRepository(IConfiguration conn)
        {
            _conn = conn;
        }

        public async Task DeleteUser(int id)
        {
            using var connect = GetConnection();
            await connect.ExecuteAsync("DELETE FROM Users WHERE Id=@Id", new { Id = id });
        }

        public async Task<List<Users>> GetAllUser()
        {
            using var connect = GetConnection();
            var sql = await connect.QueryAsync<Users>("SELECT * FROM Users");
            return sql.ToList();
        }

        public async Task<Users> GetByEmailAsynce(string email)
        {
            using var connect = GetConnection();
            var sql = await connect.QueryFirstOrDefaultAsync<Users>("SELECT * FROM Users WHERE Email=@Email", new { Email = email });
            return sql;
        }

        public async Task InsertAsync(Users users)
        {
            using var connect = GetConnection();
            var sql = "INSERT INTO Users (Name, Email, PasswordHash, Role, CreatedAt) VALUES (@Name, @Email, @PasswordHash, @Role, @CreatedAt) RETURNING Id;";
            var newId = await connect.ExecuteScalarAsync<int>(sql, users);
            users.Id = newId;

        }

        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_conn.GetConnectionString("DBConnection"));
        }
    }
}
