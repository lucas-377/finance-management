using FinanceManagement.Mapping;
using Microsoft.EntityFrameworkCore;

namespace FinanceManagement.Models.Entities
{
    public class Context : DbContext
    {
        // DbSet of models.
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<Salary> Salaries { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
        }

        // Method to add mapped class in context.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExpenseTypeMap());
            modelBuilder.ApplyConfiguration(new ExpenseMap());
            modelBuilder.ApplyConfiguration(new SalaryMap());
            modelBuilder.ApplyConfiguration(new MonthMap());
        }
    }
}
