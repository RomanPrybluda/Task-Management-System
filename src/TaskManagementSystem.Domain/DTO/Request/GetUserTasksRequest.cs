using TaskManagementSystem.Domain.Enums;
using TaskManagementSystem.Domain.Sorting;

namespace TaskManagementSystem.Domain.DTO.Request
{
    public class GetUserTasksRequest
    {
        public DateTime? DueDate { get; set; }

        public UserStatus? Status { get; set; }

        public UserTaskPriority? Priority { get; set; }

        public SortByProperty? SortByProperty { get; set; }

        public SortOrder OrderBy { get; set; } = SortOrder.Descending;

    }
}
