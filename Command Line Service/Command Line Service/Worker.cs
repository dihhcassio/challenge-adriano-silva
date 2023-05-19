using Command_Line_Domain.Communication.Exceptions;
using Command_Line_Domain.Communication.Services;
using Command_Line_Domain.WindowsInformations.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Command_Line_Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICommunicationService _service;

        public Worker(ILogger<Worker> logger, ICommunicationService service)
        {
            _logger = logger;
            _service = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);




            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _service.RegisterCommandLineService();

                    await _service.NotifyOnline();

                    _logger.LogInformation("Read new command: {time}", DateTimeOffset.Now);
                    await _service.ExecuteCommands();

                    await Task.Delay(1000, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error  on ExecuteAsync", ex);

                    if (ex is CommandListNullException || ex is HttpRequestException )
                    {
                        _logger.LogInformation("Process will be continue ");
                    }
                    else
                    {
                        throw;
                    }
                }
            }

        }
    }
}
