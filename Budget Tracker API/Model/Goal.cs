using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Budget_Tracker_API.Model
{
    public class Goal
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int GoalId { get; set; }

        [Required]
        public int UserId { get; set; }              // FK → User

        [Required]
        [MaxLength(150)]
        public string GoalName { get; set; } = string.Empty;

        [Required]
        public decimal TargetAmount { get; set; }

        [Required]
        public decimal CurrentAmount { get; set; } = 0;

        public DateTime? Deadline { get; set; }

        // Navigation property
        public User? User { get; set; }
    }
}
