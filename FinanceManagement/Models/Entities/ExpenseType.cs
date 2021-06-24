using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinanceManagement.Models.Entities
{
    public class ExpenseType
    {
        public int ExpenseTypeId { get; set; }
        [Required(ErrorMessage = "Please enter a name.")]
        [StringLength(50, ErrorMessage = "Max length 50 characters.")]
        [Remote("ExpenseTypeExists", "ExpenseTypes")]
        public string Name { get; set; }
        public ICollection<Expense> Expenses { get; set; } // A type can have many expenses.
    }
}
