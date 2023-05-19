using Command_Line_Domain.CommandLine.CommandExecuter;
using Command_Line_Domain.CommandLine.Services;
using Command_Line_Domain.Communication.Acls.CommunicationApi;
using Command_Line_Domain.Communication.Acls.CommunicationApi.Configurations;
using Command_Line_Domain.Communication.Services;
using Command_Line_Domain.WindowsInformations.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Command_Line_Ioc
{
    public static class Ioc
    {
        public static IServiceCollection ConfigureModules(this IServiceCollection services, IConfiguration configuration)
        {
            var communicationApiConfig = configuration.GetSection("CommunicationApi").Get<CommunicationApiConfig>();
            services.AddSingleton(communicationApiConfig);

            services.AddTransient<ICommunicationService, CommunicationService>();
            services.AddTransient<ICommunicationApiAcl, CommunicationApiAcl>();
            services.AddTransient<ICommandLineService, CommandLineService>();
            services.AddTransient<IWindowsInformationService, WindowsInformationService>();
            services.AddTransient<ICommandExecuter, CommadExecuter>();

            return services;
        }
    }
}
