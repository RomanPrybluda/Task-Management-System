using TMS.Domain.Entities;
using TMS.Domain.Enums;

namespace TMS.Domain.DTO.Response
{
    public class CreateUserTaskResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; }

        public UserStatus Status { get; set; }

        public UserTaskPriority Priority { get; set; }

        public static CreateUserTaskResponse UserTaskToCreateUserTaskResponse(UserTask userTask)
        {
            return new CreateUserTaskResponse
            {
                Id = userTask.Id,
                Title = userTask.Title,
                Description = userTask.Description,
                DueDate = userTask.DueDate,
                Status = userTask.Status,
                Priority = userTask.Priority,
            };
        }
    }
}
