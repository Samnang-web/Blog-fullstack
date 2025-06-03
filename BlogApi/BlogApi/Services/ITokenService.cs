using BlogApi.Models;

namespace BlogApi.Services
{
    public interface ITokenService
    {
        string GenerateToken(Users user);
    }
}
