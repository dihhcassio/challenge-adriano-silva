using Command_Line_Api_Data.CommandLine.Repositories;
using Command_Line_Api_Data.Configurations;
using Command_Line_Api_Domain.CommandLine.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Command_Line_Api_IoC.Modules
{
    public static class DataModule
    {
        public static void ConfigureDataModule(this IServiceCollection services, IConfiguration configuration) 
        {
            var dataConfig = configuration.GetSection("DataConfig").Get<DataConfig>();
            services.AddSingleton(dataConfig);

            services.AddDbContext<ApplicationDbContext>();

            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<ICommandRepository, CommandRespository>();
            services.AddTransient<ICommandReturnRepository, CommandReturnRepository>();
        }
    }
}
