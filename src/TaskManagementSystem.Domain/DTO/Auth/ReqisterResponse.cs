namespace TaskManagementSystem.Domain.DTO.Auth
{
    public class ReqisterResponse
    {
        public Guid UserId { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
