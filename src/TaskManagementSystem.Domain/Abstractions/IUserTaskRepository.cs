using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Domain.Abstractions
{
    public interface IUserTaskRepository
    {
        Task<IEnumerable<UserTask>> GetUserTasksAsync(Guid userId);

        Task<UserTask> GetUserTaskByIdAsync(Guid userTaskId, Guid userId);

        Task<UserTask> CreateUserTaskAsync(UserTask task, Guid userId);

        Task<UserTask> UpdateUserTaskAsync(UserTask task);

        Task DeleteUserTaskAsync(UserTask task);
    }
}
