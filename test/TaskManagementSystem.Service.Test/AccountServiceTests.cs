using Moq;
using TaskManagementSystem.Domain.Abstractions;
using TaskManagementSystem.Domain.Auth.DTO;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Exceptions;
using TaskManagementSystem.Service.Account;

namespace TaskManagementSystem.Service.Test
{
    public class AccountServiceTests
    {
        [Fact]
        public async Task RegisterAsync_ShouldReturnSuccess_WhenUserIsNotExist()
        {
            // Arrange
            var mockUserRepo = new Mock<IUserRepository>();
            var mockJwtService = new Mock<IJwtService>();
            var service = new AccountService(mockUserRepo.Object, mockJwtService.Object);
            var registerRequest = new Register { UserName = "newUser", Email = "user@example.com", Password = "Password123" };
            var createdUser = new User { Id = Guid.NewGuid(), UserName = "newUser", Email = "user@example.com" };

            mockUserRepo.Setup(repo => repo.UserExistsAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            mockUserRepo.Setup(repo => repo.CreateUserAsync(It.IsAny<User>())).ReturnsAsync(createdUser);
            mockUserRepo.Setup(repo => repo.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(createdUser);

            // Act
            var response = await service.RegisterAsync(registerRequest);

            // Assert
            Assert.True(response.Success);
            Assert.Equal("Registration successful.", response.Message);
        }

        [Fact]
        public async Task RegisterAsync_ShouldThrowUserAlreadyExistException_WhenUserExists()
        {
            // Arrange
            var mockUserRepo = new Mock<IUserRepository>();
            var mockJwtService = new Mock<IJwtService>();
            var service = new AccountService(mockUserRepo.Object, mockJwtService.Object);
            var registerRequest = new Register { UserName = "existingUser", Email = "existing@example.com", Password = "Password123" };

            mockUserRepo.Setup(repo => repo.UserExistsAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<CustomException>(() => service.RegisterAsync(registerRequest));
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnSuccess_WhenCredentialsAreValid()
        {
            // Arrange
            var mockUserRepo = new Mock<IUserRepository>();
            var mockJwtService = new Mock<IJwtService>();
            var service = new AccountService(mockUserRepo.Object, mockJwtService.Object);
            var loginRequest = new LoginRequest { UserName = "validUser", Password = "Password123" };

            var user = new User { Id = Guid.NewGuid(), UserName = "validUser", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123") };
            mockUserRepo.Setup(repo => repo.GetUserByUserNameOrEmailAsync(It.IsAny<LoginRequest>())).ReturnsAsync(user);
            mockJwtService.Setup(jwt => jwt.GenerateJwtToken(It.IsAny<User>())).Returns("mockJwtToken");

            // Act
            var response = await service.LoginAsync(loginRequest);

            // Assert
            Assert.True(response.Success);
            Assert.Equal("LoginRequest successful.", response.Message);
            Assert.NotNull(response.AccessToken);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnFailure_WhenCredentialsAreInvalid()
        {
            // Arrange
            var mockUserRepo = new Mock<IUserRepository>();
            var mockJwtService = new Mock<IJwtService>();
            var service = new AccountService(mockUserRepo.Object, mockJwtService.Object);
            var loginRequest = new LoginRequest { UserName = "invalidUser", Password = "WrongPassword" };

            mockUserRepo.Setup(repo => repo.GetUserByUserNameOrEmailAsync(It.IsAny<LoginRequest>())).ReturnsAsync((User)null);

            // Act
            var response = await service.LoginAsync(loginRequest);

            // Assert
            Assert.False(response.Success);
            Assert.Equal("LoginRequest failed", response.Message);
        }

    }
}