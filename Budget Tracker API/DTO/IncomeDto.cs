using System.ComponentModel.DataAnnotations;

namespace Budget_Tracker_API.DTO
{
    public class IncomeDto
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(150)]
        public string SourceName { get; set; } = string.Empty;

        [Required]
        public DateTime IncomeDate { get; set; }

        public bool IsRecurring { get; set; } = false;
    }
}
