using BlogApi.DTOs;
using BlogApi.Repository;
using BCrypt.Net;
using BlogApi.Models;

namespace BlogApi.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepo;
        private readonly ITokenService _tokenService;
        public UserService(IUserRepository userRepo, ITokenService tokenService)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
        }

        public async Task<UserLoginResponseDto> LoginAsync(string email, string password)
        {
            var user = await _userRepo.GetByEmailAsynce(email);
            if (user == null)
                return null;

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (!isValidPassword)
                return null;

            var token = _tokenService.GenerateToken(user);

            return new UserLoginResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Token = token
            };
        }

        public async Task<bool> RegisterUserAsync(UserDTo dto)
        {
            // Hash password using BCrypt
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new Users
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = passwordHash,
                Role = dto.Role ?? "User",  // default role
                CreatedAt = DateTime.UtcNow
            };

            await _userRepo.InsertAsync(user);

            return true;
        }

        internal object GenerateJwtToken(UserLoginResponseDto user)
        {
            throw new NotImplementedException();
        }
    }
}
