using System.ComponentModel.DataAnnotations;

namespace Budget_Tracker_API.DTO
{
    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
