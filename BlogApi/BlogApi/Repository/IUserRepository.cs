using BlogApi.Models;

namespace BlogApi.Repository
{
    public interface IUserRepository
    {
        Task<List<Users>> GetAllUser();
        Task<Users> GetByEmailAsynce(string email);
        Task InsertAsync(Users users);
        Task DeleteUser(int id);
    }
}
