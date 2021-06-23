using FinanceManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceManagement.Mapping
{
    // Map models to database schema.
    public class MonthMap : IEntityTypeConfiguration<Month>
    {
        public void Configure(EntityTypeBuilder<Month> builder)
        {
            builder.HasKey(m => m.MonthId); // PK.
            builder.Property(m => m.MonthId).ValueGeneratedNever(); // We set the key, disable automatic generated value.
            builder.Property(m => m.Name).HasMaxLength(50).IsRequired();

            builder.HasMany(m => m.Expenses).WithOne(m => m.Month).HasForeignKey(m => m.MonthId).OnDelete(DeleteBehavior.Cascade); // FK to month, delete everything.

            builder.HasOne(m => m.Salaries).WithOne(m => m.Month).OnDelete(DeleteBehavior.Cascade); // Relationship 1 to 1.

            builder.ToTable("Months"); // Create table.
        }
    }
}
