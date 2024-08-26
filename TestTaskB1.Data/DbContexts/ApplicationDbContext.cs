using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTaskB1.Data.Configuration;
using TestTaskB1.Domain.Models;

namespace TestTaskB1.Data.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<DataRecord> DataRecords { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<ClassModel> ClassModels { get; set; }
        public DbSet<FileUploadModel> FileUploadModels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new ClassModelConfiguration());
            modelBuilder.ApplyConfiguration(new UploadFileModelConfiguration());

            modelBuilder.ApplyConfiguration(new DataRecordConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
