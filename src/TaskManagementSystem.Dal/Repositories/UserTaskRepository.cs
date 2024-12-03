using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Domain.Abstractions;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Dal.Repositories
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private readonly TaskManagmentSystemDbContext context;

        public UserTaskRepository(TaskManagmentSystemDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<UserTask>> GetUserTasksAsync(Guid userId)
        {
            return await context.UserTasks.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<UserTask> GetUserTaskByIdAsync(Guid userTaskId, Guid userId)
        {
            return await context.UserTasks
                .Where(t => t.Id == userTaskId && t.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<UserTask> CreateUserTaskAsync(UserTask task, Guid userId)
        {
            context.UserTasks.Add(task);
            await context.SaveChangesAsync();
            return task;
        }

        public async Task<UserTask> UpdateUserTaskAsync(UserTask task)
        {
            context.UserTasks.Update(task);
            await context.SaveChangesAsync();
            return task;
        }

        public async Task DeleteUserTaskAsync(UserTask task)
        {
            context.UserTasks.Remove(task);
            await context.SaveChangesAsync();
        }

    }
}
