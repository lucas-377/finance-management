using FinanceManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceManagement.Mapping
{
    public class ExpenseMap : IEntityTypeConfiguration<Expense>
    {
        // Map models to database schema.
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(e => e.ExpenseId); // PK
            builder.Property(e => e.Value).IsRequired();

            builder.HasOne(e => e.Month).WithMany(e => e.Expenses).HasForeignKey(e => e.MonthId); // FK to month.
            builder.HasOne(e => e.ExpenseTypes).WithMany(e => e.Expenses).HasForeignKey(e => e.ExpenseTypeId); //FK to expense type.

            builder.ToTable("Expenses");
        }
    }
}
