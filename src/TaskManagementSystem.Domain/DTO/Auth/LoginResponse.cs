namespace TaskManagementSystem.Domain.DTO.Auth
{
    public class LoginResponse
    {
        public Guid UserId { get; set; }

        public bool Success { get; set; }

        public string AccessToken { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;
    }
}
