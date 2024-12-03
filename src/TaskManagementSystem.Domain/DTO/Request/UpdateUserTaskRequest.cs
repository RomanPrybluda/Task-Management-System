using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.DTO.Request
{
    public class UpdateUserTaskRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime? DueDate { get; set; }

        [Required]
        public UserStatus Status { get; set; }

        [Required]
        public UserTaskPriority Priority { get; set; }
    }
}
