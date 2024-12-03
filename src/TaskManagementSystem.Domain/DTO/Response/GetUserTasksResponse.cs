using TMS.Domain.Entities;
using TMS.Domain.Enums;

namespace TMS.Domain.DTO.Response
{
    public class GetUserTasksResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; }

        public UserStatus Status { get; set; }

        public UserTaskPriority Priority { get; set; }

        public static GetUserTasksResponse UserTaskToUserTaskResponse(UserTask userTask)
        {

            return new GetUserTasksResponse
            {
                Id = userTask.Id,
                Title = userTask.Title,
                Description = userTask.Description,
                DueDate = userTask.DueDate,
                Status = userTask.Status,
                Priority = userTask.Priority
            };
        }
    }
}
