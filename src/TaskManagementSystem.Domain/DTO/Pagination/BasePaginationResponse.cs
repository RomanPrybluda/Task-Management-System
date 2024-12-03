using TMS.Domain.DTO.Response;

namespace TMS.Domain.DTO.Pagination
{
    public class PaginationResponse : PaginationRequest
    {

        public int TotalPages { get; init; }

        public int TotalTasks { get; init; }

        public List<GetUserTasksResponse>? UserTasks { get; init; }
    }
}
