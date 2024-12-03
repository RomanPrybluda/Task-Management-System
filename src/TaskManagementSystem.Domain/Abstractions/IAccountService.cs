using TMS.Domain.Auth.DTO;
using TMS.Domain.DTO.Auth;

namespace TMS.Domain.Abstractions
{
    public interface IAccountService
    {
        Task<ReqisterResponse> RegisterAsync(Register request);

        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
