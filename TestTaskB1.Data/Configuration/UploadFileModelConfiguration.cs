using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskB1.Domain.Models;

namespace TestTaskB1.Data.Configuration
{
    public class UploadFileModelConfiguration : IEntityTypeConfiguration<FileUploadModel>
    {
        public void Configure(EntityTypeBuilder<FileUploadModel> builder)
        {
            builder.ToTable(nameof(FileUploadModel));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FileName)
                .IsRequired()
                .HasMaxLength(255); 

            builder.Property(e => e.UploadDate)
                .IsRequired();
        }
    }
}