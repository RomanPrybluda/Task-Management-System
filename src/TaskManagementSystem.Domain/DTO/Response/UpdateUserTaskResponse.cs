using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.DTO.Response
{
    public class UpdateUserTaskResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; }

        public UserStatus Status { get; set; }

        public UserTaskPriority Priority { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public static UpdateUserTaskResponse UserTaskToUpdateUserTaskResponse(UserTask userTask)
        {

            return new UpdateUserTaskResponse
            {
                Id = userTask.Id,
                Title = userTask.Title,
                Description = userTask.Description,
                DueDate = userTask.DueDate,
                Status = userTask.Status,
                Priority = userTask.Priority,
                CreatedAt = userTask.CreatedAt,
                UpdatedAt = userTask.UpdatedAt,
            };
        }
    }
}
