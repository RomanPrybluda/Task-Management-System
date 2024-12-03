using TaskManagementSystem.Domain.Auth.DTO;
using TaskManagementSystem.Domain.DTO.Auth;

namespace TaskManagementSystem.Domain.Abstractions
{
    public interface IAccountService
    {
        Task<ReqisterResponse> RegisterAsync(Register request);

        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
