using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Budget_Tracker_API.Model
{
    public class Income
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int IncomeId { get; set; }

        [Required]
        public int UserId { get; set; }              // FK → User

        [Required]
        [MaxLength(150)]
        public string SourceName { get; set; } = string.Empty;

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime IncomeDate { get; set; }

        public bool IsRecurring { get; set; } = false;

        // Navigation property
        public User? User { get; set; }
    }
}
