using TaskManagementSystem.Domain.Abstractions;
using TaskManagementSystem.Domain.DTO.Pagination;
using TaskManagementSystem.Domain.DTO.Request;
using TaskManagementSystem.Domain.DTO.Response;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Exceptions;
using TaskManagementSystem.Domain.Sorting;

namespace TaskManagementSystem.Service
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IUserTaskRepository userTaskRepo;
        private readonly IUserRepository userRepo;

        public UserTaskService(IUserTaskRepository userTaskRepo, IUserRepository userRepo)
        {
            this.userTaskRepo = userTaskRepo;
            this.userRepo = userRepo;
        }

        public async Task<PaginationResponse> GetUserTasksAsync(
            Guid userId,
            GetUserTasksRequest getUserTasksRequest,
            PaginationRequest paginationRequest)
        {
            paginationRequest.PageSize = paginationRequest.PageSize <= 0 ? 10 : paginationRequest.PageSize;

            paginationRequest.PageNumber = paginationRequest.PageNumber <= 0 ? 1 : paginationRequest.PageNumber;

            var user = await userRepo.GetUserByIdAsync(userId);
            if (user == null)
                throw new CustomException(CustomExceptionType.UserNotFound, $"User with ID:{userId} not found.");

            var query = (await userTaskRepo.GetUserTasksAsync(userId)).AsQueryable();

            if (getUserTasksRequest.DueDate.HasValue)
            {
                query = query.Where(t => t.DueDate == getUserTasksRequest.DueDate.Value);
            }

            if (getUserTasksRequest.Status.HasValue)
            {
                query = query.Where(t => t.Status == getUserTasksRequest.Status.Value);
            }

            if (getUserTasksRequest.Priority.HasValue)
            {
                query = query.Where(t => t.Priority == getUserTasksRequest.Priority.Value);
            }

            if (getUserTasksRequest.SortByProperty.HasValue)
            {
                switch (getUserTasksRequest.SortByProperty.Value)
                {
                    case SortByProperty.DueDate:
                        query = getUserTasksRequest.OrderBy == SortOrder.Ascending
                            ? query.OrderBy(t => t.DueDate)
                            : query.OrderByDescending(t => t.DueDate);
                        break;
                    case SortByProperty.Status:
                        query = getUserTasksRequest.OrderBy == SortOrder.Ascending
                            ? query.OrderBy(t => t.Status)
                            : query.OrderByDescending(t => t.Status);
                        break;
                    case SortByProperty.Priority:
                        query = getUserTasksRequest.OrderBy == SortOrder.Ascending
                            ? query.OrderBy(t => t.Priority)
                            : query.OrderByDescending(t => t.Priority);
                        break;
                    default:
                        break;
                }
            }

            var totalTasks = query.Count();

            var totalPages = totalTasks > 0 ? (int)Math.Ceiling(totalTasks / (double)paginationRequest.PageSize) : 0;

            paginationRequest.PageNumber = paginationRequest.PageNumber > totalPages ? totalPages : paginationRequest.PageNumber;

            var skip = (paginationRequest.PageNumber - 1) * paginationRequest.PageSize;
            query = query.Skip(skip).Take(paginationRequest.PageSize);
            var userTasks = query.ToList();

            var userTasksResponse = userTasks.Select(t => GetUserTasksResponse.UserTaskToUserTaskResponse(t)).ToList();

            return new PaginationResponse
            {
                PageNumber = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize,
                TotalPages = totalPages,
                TotalTasks = totalTasks,
                UserTasks = userTasksResponse
            };
        }

        public async Task<GetUserTaskByIdResponse> GetUserTaskByIdAsync(Guid userTaskId, Guid userId)
        {
            var user = await userRepo.GetUserByIdAsync(userId);
            if (user == null)
                throw new CustomException(CustomExceptionType.UserNotFound, $"User with ID:{userId} not found.");

            var userTask = await userTaskRepo.GetUserTaskByIdAsync(userTaskId, userId);
            if (userTask == null)
                throw new CustomException(CustomExceptionType.TaskNotFound, $"User task with ID:{userTaskId} not found.");

            var userTasksByIdResponse = GetUserTaskByIdResponse.UserTaskToUserTaskByIdResponse(userTask);
            return userTasksByIdResponse;
        }

        public async Task<CreateUserTaskResponse> CreateUserTaskAsync(CreateUserTaskRequest request, Guid userId)
        {
            var user = await userRepo.GetUserByIdAsync(userId);
            if (user == null)
                throw new CustomException(CustomExceptionType.UserNotFound, $"User with ID:{userId} not found.");

            var task = new UserTask
            {
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                DueDate = request.DueDate,
                Priority = request.Priority,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdTask = await userTaskRepo.CreateUserTaskAsync(task, userId);

            var createUserTaskResponse = CreateUserTaskResponse.UserTaskToCreateUserTaskResponse(createdTask);
            return createUserTaskResponse;
        }

        public async Task<UpdateUserTaskResponse> UpdateUserTaskAsync(UpdateUserTaskRequest request, Guid userId)
        {
            var user = await userRepo.GetUserByIdAsync(userId);
            if (user == null)
                throw new CustomException(CustomExceptionType.UserNotFound, $"User with ID:{userId} not found.");

            var userTask = await userTaskRepo.GetUserTaskByIdAsync(request.Id, userId);
            if (userTask == null)
                throw new CustomException(CustomExceptionType.TaskNotFound, $"User task with ID:{userTask} not found.");

            userTask.Title = request.Title;
            userTask.Description = request.Description;
            userTask.Status = request.Status;
            userTask.DueDate = request.DueDate;
            userTask.Priority = request.Priority;
            userTask.UpdatedAt = DateTime.UtcNow;

            var updatedUserTask = await userTaskRepo.UpdateUserTaskAsync(userTask);

            var updateUserTaskResponse = UpdateUserTaskResponse.UserTaskToUpdateUserTaskResponse(updatedUserTask);
            return updateUserTaskResponse;
        }

        public async Task DeleteUserTaskAsync(Guid userTaskId, Guid userId)
        {
            var task = await userTaskRepo.GetUserTaskByIdAsync(userTaskId, userId);
            if (task == null)
                throw new CustomException(CustomExceptionType.TaskNotFound, $"User task with ID:{userTaskId} not found.");

            await userTaskRepo.DeleteUserTaskAsync(task);
        }
    }
}