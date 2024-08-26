using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskB1.Domain.Models;

namespace TestTaskB1.Data.Configuration
{
    public class ClassModelConfiguration : IEntityTypeConfiguration<ClassModel>
    {
        public void Configure(EntityTypeBuilder<ClassModel> builder)
        {
            builder.ToTable(nameof(ClassModel));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.IncomingBalance)
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.Debit)
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.Credit)
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.OutgoingBalance)
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.IncomingPassive)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.OutgoingPassive)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasMany(e => e.Accounts)
                .WithOne()
                .HasForeignKey(a => a.ClassId); 
        }
    }
}