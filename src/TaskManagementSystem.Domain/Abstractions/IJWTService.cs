using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Domain.Abstractions
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user);
    }
}
