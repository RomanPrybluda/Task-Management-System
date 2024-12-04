using Microsoft.EntityFrameworkCore;
using Moq;
using TaskManagementSystem.Dal.Repositories;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Dal.Test
{
    public class UserRepositoryTests
    {
        private readonly UserRepository _userRepository;
        private readonly Mock<TaskManagmentSystemDbContext> _mockContext;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<TaskManagmentSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "TMSDatabase")
                .Options;

            var context = new TaskManagmentSystemDbContext(options);
            _userRepository = new UserRepository(context);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, UserName = "testuser", Email = "test@example.com" };
            _userRepository.CreateUserAsync(user);

            // Act
            var result = await _userRepository.GetUserByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            var result = await _userRepository.GetUserByIdAsync(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateUserAsync_ShouldAddUser_WhenValidUser()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), UserName = "newuser", Email = "new@example.com" };

            // Act
            var createdUser = await _userRepository.CreateUserAsync(user);

            // Assert
            Assert.NotNull(createdUser);
            Assert.Equal(user.UserName, createdUser.UserName);
            Assert.Equal(user.Email, createdUser.Email);
        }

        [Fact]
        public async Task UserExistsAsync_ShouldReturnTrue_WhenUserExists()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), UserName = "existinguser", Email = "existing@example.com" };
            await _userRepository.CreateUserAsync(user);

            // Act
            var result = await _userRepository.UserExistsAsync("existinguser", "existing@example.com");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UserExistsAsync_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            // Act
            var result = await _userRepository.UserExistsAsync("nonexistentuser", "nonexistent@example.com");

            // Assert
            Assert.False(result);
        }
    }

}