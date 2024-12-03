using TaskManagementSystem.Domain.DTO.Pagination;
using TaskManagementSystem.Domain.DTO.Request;
using TaskManagementSystem.Domain.DTO.Response;

namespace TaskManagementSystem.Domain.Abstractions
{
    public interface IUserTaskService
    {
        Task<PaginationResponse> GetUserTasksAsync(Guid userId, GetUserTasksRequest getUserTasksRequest, PaginationRequest paginationRequest);

        Task<GetUserTaskByIdResponse> GetUserTaskByIdAsync(Guid userTaskId, Guid userId);

        Task<CreateUserTaskResponse> CreateUserTaskAsync(CreateUserTaskRequest task, Guid userId);

        Task<UpdateUserTaskResponse> UpdateUserTaskAsync(UpdateUserTaskRequest task, Guid userId);

        Task DeleteUserTaskAsync(Guid taskId, Guid userId);

    }
}
