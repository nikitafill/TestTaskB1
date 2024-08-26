using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskB1.Domain.Models;

namespace TestTaskB1.Data.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable(nameof(Account));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.ClassId)
                .IsRequired();

            builder.Property(e => e.AccountNumber)
                .IsRequired()
                .HasMaxLength(50); 

            builder.Property(e => e.IncomingBalance)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.Debit)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.Credit)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.OutgoingBalance)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.IncomingPassive)
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.OutgoingPassive)
                .HasColumnType("decimal(18,2)");
        }
    }
}