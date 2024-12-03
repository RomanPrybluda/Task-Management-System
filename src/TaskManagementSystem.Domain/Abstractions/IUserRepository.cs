using TMS.Domain.Auth.DTO;
using TMS.Domain.Entities;

namespace TMS.Domain.Abstractions
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
