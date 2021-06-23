using System.Collections.Generic;

namespace FinanceManagement.Models.Entities
{
    public class Month
    {
        public int MonthId { get; set; }
        public string Name { get; set; }
        public ICollection<Expense> Expenses { get; set; } // A month can have many expenses.
        public Salary Salaries { get; set; } // Associate salary class.
    }
}
