using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Domain.Abstractions;
using TaskManagementSystem.Domain.Auth.DTO;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Dal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskManagmentSystemDbContext context;

        public UserRepository(TaskManagmentSystemDbContext context)
        {
            this.context = context;
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            var user = await context.Users.FindAsync(userId);
            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<User> GetUserByUserNameOrEmailAsync(LoginRequest request)
        {
            var user = await context.Users
                .FirstOrDefaultAsync
                (u => u.UserName == request.UserName || u.PasswordHash == request.Password);
            return user;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var createdUser = await context.Users.FindAsync(user.Id);

            return createdUser;
        }

        public async Task<bool> UserExistsAsync(string userName, string email)
        {
            return await context.Users.AnyAsync(u => u.UserName == userName || u.Email == email);
        }
    }
}
