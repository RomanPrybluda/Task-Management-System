using TaskManagementSystem.Domain.Auth.DTO;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Domain.Abstractions
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(Guid userId);

        Task<User> GetUserByUserNameOrEmailAsync(LoginRequest request);

        Task<User> GetUserByEmailAsync(string email);

        Task<User> CreateUserAsync(User user);

        Task<bool> UserExistsAsync(string userName, string email);

    }
}
