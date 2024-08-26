using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskB1.Domain.Models;

namespace TestTaskB1.Data.Configuration
{
    public class DataRecordConfiguration : IEntityTypeConfiguration<DataRecord>
    {
        public void Configure(EntityTypeBuilder<DataRecord> builder)
        {
            builder.ToTable(nameof(DataRecord));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.RecordDate)
                .IsRequired(); 

            builder.Property(e => e.LatinCharacters)
                .IsRequired()
                .HasMaxLength(10); 

            builder.Property(e => e.CyrillicCharacters)
                .IsRequired()
                .HasMaxLength(10); 

            builder.Property(e => e.EvenInteger)
                .IsRequired(); 

            builder.Property(e => e.DecimalNumber)
                .IsRequired()
                .HasColumnType("decimal(18,8)"); 
        }
    }
}