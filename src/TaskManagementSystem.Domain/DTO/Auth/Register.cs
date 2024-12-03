using System.ComponentModel.DataAnnotations;
using TMS.Domain.Attribute;

namespace TMS.Domain.Auth.DTO
{
    public class Register
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        [ContainsSpecialCharacter]
        public string Password { get; set; } = string.Empty;
    }
}
