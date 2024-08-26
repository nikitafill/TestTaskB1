using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TestTaskB1.Service.Services.Interfaces;
using TestTaskB1.Service.Services;

namespace TestTaskB1.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IDataProcessingService, DataProcessingService>();
            services.AddScoped<IOcbService, OcbService>();
            return services;
        }
    }
}