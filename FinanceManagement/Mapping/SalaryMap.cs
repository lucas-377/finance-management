using FinanceManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceManagement.Mapping
{
    // Map models to database schema.
    public class SalaryMap : IEntityTypeConfiguration<Salary>
    {
        public void Configure(EntityTypeBuilder<Salary> builder)
        {
            builder.HasKey(s => s.SalaryId); // PK.
            builder.Property(s => s.Value).IsRequired();

            builder.HasOne(s => s.Month).WithOne(s => s.Salaries).HasForeignKey<Salary>(s => s.MonthId); // FK to month.

            builder.ToTable("Salaries");
        }
    }
}
