using TMS.Domain.DTO.Pagination;
using TMS.Domain.DTO.Request;
using TMS.Domain.DTO.Response;

namespace TMS.Domain.Abstractions
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
