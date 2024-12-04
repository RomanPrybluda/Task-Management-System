using Moq;
using TaskManagementSystem.Domain.Abstractions;
using TaskManagementSystem.Domain.DTO.Pagination;
using TaskManagementSystem.Domain.DTO.Request;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Exceptions;

namespace TaskManagementSystem.Service.Test
{
    public class UserTaskServiceTests
    {
        [Fact]
        public async Task GetUserTasksAsync_ShouldReturnTasks_WhenValidUserIdAndRequest()
        {
            // Arrange
            var mockUserRepo = new Mock<IUserRepository>();
            var mockUserTaskRepo = new Mock<IUserTaskRepository>();
            var service = new UserTaskService(mockUserTaskRepo.Object, mockUserRepo.Object);
            var userId = Guid.NewGuid();
            var paginationRequest = new PaginationRequest { PageSize = 10, PageNumber = 1 };
            var getUserTasksRequest = new GetUserTasksRequest();

            mockUserRepo.Setup(repo => repo.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new User { Id = userId });
            mockUserTaskRepo.Setup(repo => repo.GetUserTasksAsync(It.IsAny<Guid>())).ReturnsAsync(new List<Domain.Entities.UserTask> { new Domain.Entities.UserTask { Id = Guid.NewGuid(), Title = "Task1" } });

            // Act
            var response = await service.GetUserTasksAsync(userId, getUserTasksRequest, paginationRequest);

            // Assert
            Assert.NotNull(response);
            Assert.Single(response.UserTasks);
        }

        [Fact]
        public async Task GetUserTasksAsync_ShouldThrowUserNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var mockUserRepo = new Mock<IUserRepository>();
            var mockUserTaskRepo = new Mock<IUserTaskRepository>();
            var service = new UserTaskService(mockUserTaskRepo.Object, mockUserRepo.Object);
            var userId = Guid.NewGuid();
            var paginationRequest = new PaginationRequest { PageSize = 10, PageNumber = 1 };
            var getUserTasksRequest = new GetUserTasksRequest();

            mockUserRepo.Setup(repo => repo.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User)null);

            // Act & Assert
            await Assert.ThrowsAsync<CustomException>(() => service.GetUserTasksAsync(userId, getUserTasksRequest, paginationRequest));
        }

    }


}
