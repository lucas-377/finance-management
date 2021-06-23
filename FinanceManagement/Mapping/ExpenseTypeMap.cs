using FinanceManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceManagement.Mapping
{
    // Map models to database schema.
    public class ExpenseTypeMap : IEntityTypeConfiguration<ExpenseType>
    {
        public void Configure(EntityTypeBuilder<ExpenseType> builder)
        {
            builder.HasKey(et => et.ExpenseTypeId); // Configure Primary Key.
            builder.Property(et => et.Name).HasMaxLength(50).IsRequired();

            builder.HasMany(et => et.Expenses).WithOne(et => et.ExpenseTypes).HasForeignKey(et => et.ExpenseTypeId); // Relationship 1 to Many.

            builder.ToTable("ExpenseTypes"); // Create table.
        }
    }
}
