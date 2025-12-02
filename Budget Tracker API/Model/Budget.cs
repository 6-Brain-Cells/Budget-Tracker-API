using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Budget_Tracker_API.Model
{
    public class Budget
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int BudgetId { get; set; }

        [Required]
        public int UserId { get; set; }      // Foreign Key

        [Required]
        public int Month { get; set; }       // 1–12

        [Required]
        public int Year { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User? User { get; set; }
        public ICollection<BudgetCategory>? Categories { get; set; }

    }
}
