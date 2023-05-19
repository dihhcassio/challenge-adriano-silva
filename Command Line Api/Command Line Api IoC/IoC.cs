using Command_Line_Api_Data.CommandLine.Repositories;
using Command_Line_Api_IoC.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Command_Line_Api_IoC
{
    public static class IoC
    {
        public static IServiceCollection ConfigureModules(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDataModule(configuration);
            services.ConfigureDomainModule(configuration);

            return services;
        }

        public static void ConfigureDataBase(this IApplicationBuilder app) 
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}
