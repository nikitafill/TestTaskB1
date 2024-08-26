using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestTaskB1.Data.DbContexts;
using System.Reflection;

namespace TestTaskB1.Data.ContextFactory
{
    internal class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var buiilder = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseNpgsql(configuration.GetConnectionString("DbConnectionString"),
              b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));

            return new ApplicationDbContext(buiilder.Options);
        }
    }
}
