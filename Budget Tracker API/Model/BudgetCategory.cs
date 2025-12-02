using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Budget_Tracker_API.Model
{
    public class BudgetCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int CategoryId { get; set; }

        [Required]
        public int BudgetId { get; set; }    // Foreign Key

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal LimitAmount { get; set; }

        // Navigation Properties
        public Budget? Budget { get; set; }
        public ICollection<Expense>? Expenses { get; set; }
    }
}
