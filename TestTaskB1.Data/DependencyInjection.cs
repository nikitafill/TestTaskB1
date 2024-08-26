using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TestTaskB1.Data.DbContexts;
using TestTaskB1.Domain.Interfaces;

namespace TestTaskB1.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnectionString");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
                options.EnableSensitiveDataLogging();
            });
            services.AddScoped<IRepositoryManager, RepositoryManager>();

            return services;
        }
    }
}
