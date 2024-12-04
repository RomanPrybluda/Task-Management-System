using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Dal.Repositories;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Dal.Test
{
    public class UserTaskRepositoryTests
    {
        private readonly UserTaskRepository _userTaskRepository;
        private readonly TaskManagmentSystemDbContext _context;

        public UserTaskRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<TaskManagmentSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "TMSDatabase")
                .Options;

            _context = new TaskManagmentSystemDbContext(options);
            _userTaskRepository = new UserTaskRepository(_context);
        }

        [Fact]
        public async Task GetUserTasksAsync_ShouldReturnTasks_WhenUserHasTasks()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userTask = new UserTask
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Title = "Test Task",
                Status = UserStatus.Pending,
                DueDate = DateTime.UtcNow
            };
            _context.UserTasks.Add(userTask);
            await _context.SaveChangesAsync();

            // Act
            var result = await _userTaskRepository.GetUserTasksAsync(userId);

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(userId, result.First().UserId);
        }

        [Fact]
        public async Task GetUserTasksAsync_ShouldReturnEmpty_WhenUserHasNoTasks()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            var result = await _userTaskRepository.GetUserTasksAsync(userId);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task CreateUserTaskAsync_ShouldCreateTask_WhenValidTask()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var task = new UserTask
            {
                Id = Guid.NewGuid(),
                Title = "New Task",
                Description = "Task Description",
                UserId = userId,
                Status = UserStatus.Pending,
                DueDate = DateTime.UtcNow
            };

            // Act
            var createdTask = await _userTaskRepository.CreateUserTaskAsync(task, userId);

            // Assert
            Assert.NotNull(createdTask);
            Assert.Equal(task.Title, createdTask.Title);
            Assert.Equal(task.Description, createdTask.Description);
        }

        [Fact]
        public async Task DeleteUserTaskAsync_ShouldDeleteTask_WhenTaskExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var task = new UserTask
            {
                Id = Guid.NewGuid(),
                Title = "Task to Delete",
                UserId = userId,
                Status = UserStatus.Pending,
                DueDate = DateTime.UtcNow
            };
            _context.UserTasks.Add(task);
            await _context.SaveChangesAsync();

            // Act
            await _userTaskRepository.DeleteUserTaskAsync(task);

            // Assert
            var result = await _userTaskRepository.GetUserTaskByIdAsync(task.Id, userId);
            Assert.Null(result);
        }

    }

}
