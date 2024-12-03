using TaskManagementSystem.Domain.DTO.Response;

namespace TaskManagementSystem.Domain.DTO.Pagination
{
    public class PaginationResponse : PaginationRequest
    {

        public int TotalPages { get; init; }

        public int TotalTasks { get; init; }

        public List<GetUserTasksResponse>? UserTasks { get; init; }
    }
}
