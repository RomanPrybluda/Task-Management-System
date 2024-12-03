using TMS.Domain.Abstractions;
using TMS.Domain.Auth.DTO;
using TMS.Domain.DTO.Auth;
using TMS.Domain.Entities;
using TMS.Domain.Exceptions;

namespace TMS.Service
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository userRepo;
        private readonly IJwtService jwtService;

        public AccountService(IUserRepository userRepo, IJwtService jwtService)
        {
            this.userRepo = userRepo;
            this.jwtService = jwtService;
        }

        public async Task<ReqisterResponse> RegisterAsync(Register request)
        {
            var userIsExist = await userRepo.UserExistsAsync(request.UserName, request.Email);

            if (userIsExist)
                throw new CustomException(CustomExceptionType.UserAlreadyExist, "User with this Name or Email already exists");

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = HashPassword(request.Password)
            };
            await userRepo.CreateUserAsync(user);

            var createdUser = await userRepo.GetUserByEmailAsync(request.Email);

            if (createdUser == null)
                throw new CustomException(CustomExceptionType.UserCreationFailed, "User creation failed");

            var reqisterResponse = new ReqisterResponse
            {
                UserId = createdUser.Id,
                Success = true,
                Message = "Registration successful.",
            };

            return reqisterResponse;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await userRepo.GetUserByUserNameOrEmailAsync(request);

            if (user == null || !VerifyPassword(user.PasswordHash, request.Password))
                return new LoginResponse { Success = false, Message = "LoginRequest failed" };

            var token = jwtService.GenerateJwtToken(user);

            var loginResponse = new LoginResponse
            {
                UserId = user.Id,
                Success = true,
                AccessToken = token,
                Message = "LoginRequest successful."
            };

            return loginResponse;
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private static bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        }

    }

}
