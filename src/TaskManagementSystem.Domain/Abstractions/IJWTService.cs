using TMS.Domain.Entities;

namespace TMS.Domain.Abstractions
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user);
    }
}
