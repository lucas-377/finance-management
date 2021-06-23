using Microsoft.EntityFrameworkCore;

namespace FinanceManagement.Models.Entities
{
    public class Context : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<Salary> Salaries { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
        }
    }
}
