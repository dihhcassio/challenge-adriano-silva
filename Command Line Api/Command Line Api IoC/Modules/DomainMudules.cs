using Command_Line_Api_Domain.CommandLine.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_IoC
{
    public static class DomainMudules
    {
        public static void ConfigureDomainModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IClientService, ClientService>(); 
            services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<ICommandReturnService, CommandReturnService>();
        }
    }
}
