using System.ComponentModel.DataAnnotations;

namespace FinanceManagement.Models.Entities
{
    public class Salary
    {
        public int SalaryId { get; set; }
        public int MonthId { get; set; } // FK to month.
        public Month Month { get; set; }
        [Required(ErrorMessage = "Please enter a value.")]
        [Range(0, double.MaxValue, ErrorMessage = "Invalid value.")] // Dont accept negative values and double overflow value.
        public double Value { get; set; }
    }
}
