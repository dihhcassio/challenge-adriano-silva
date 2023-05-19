using Command_Line_Domain.CommandLine.Services;
using Command_Line_Domain.Communication.Acls.CommunicationApi;
using Command_Line_Domain.Communication.Dtos;
using Command_Line_Domain.Communication.Exceptions;
using Command_Line_Domain.WindowsInformations.Models;
using Command_Line_Domain.WindowsInformations.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command_Line_Domain.Communication.Services
{
    public class CommunicationService : ICommunicationService
    {
        private readonly ILogger<CommunicationService> _logger;
        private readonly ICommunicationApiAcl _communicationApiAcl;
        private readonly ICommandLineService _commandLineService;
        private readonly IWindowsInformationService _windowsInformationService;

        public CommunicationService(ILogger<CommunicationService> logger,
            ICommunicationApiAcl communicationApiAcl,
            ICommandLineService commandLineService,
            IWindowsInformationService windowsInformationService)
        {
            _logger = logger;
            _communicationApiAcl = communicationApiAcl;
            _commandLineService = commandLineService;
            _windowsInformationService = windowsInformationService;

        }

        public async Task ExecuteCommands()
        {
            var windowsInformation = _windowsInformationService.LoadWindowsInformations();

            if (windowsInformation == null) throw new WindowsInformationsNullException();

            var commandList = await _communicationApiAcl.GetCommands(windowsInformation.MacAddress);

            if (commandList == null) throw new CommandListNullException();


            foreach (CommandDto command in commandList)
            {
                try
                {
                    var commandReturnContent = _commandLineService.RunCommand(command.Content);

                    _communicationApiAcl.SendReturn(new CommandReturnSaveDto()
                    {
                        CommandId = command.Id,
                        Content = commandReturnContent
                    });
                }
                catch (Exception e) 
                {
                    _communicationApiAcl.SendReturn(new CommandReturnSaveDto()
                    {
                        CommandId = command.Id,
                        Content = $"Error: {e.Message}"
                    });
                }
            }

            

        }

        public async Task RegisterCommandLineService()
        {
            var windowsInformations = _windowsInformationService.LoadWindowsInformations();

            if (windowsInformations == null) throw new WindowsInformationsNullException();

            await _communicationApiAcl.RegistryService(new RegistryServiceDto()
            {
                MacAddress = windowsInformations.MacAddress,
                AntivirusList = windowsInformations.AntivirusList,
                DotNetVersion = windowsInformations.DotNetVersion,
                HostName = windowsInformations.HostName,
                IpAddress = windowsInformations.IpAddress,
                IsFirewallActive = windowsInformations.IsFirewallActive,
                OsVersion = windowsInformations.OsVersion,
                HardDrives = windowsInformations.HardDrives?.Select(hardDrive => new RegistryHardDriveDto()
                {
                    Name = hardDrive.Name,
                    TotalFreeSpace = hardDrive.TotalFreeSpace,
                    TotalSize = hardDrive.TotalSize
                }).ToList()
            });
        }

        public async Task NotifyOnline() 
        {
            var windowsInformations = _windowsInformationService.LoadWindowsInformations();

            if (windowsInformations == null) throw new WindowsInformationsNullException();

            await _communicationApiAcl.NotifyOnline(new NotifyOnlineDto() 
            {
                MacAddress = windowsInformations.MacAddress
            });

        }
    }
}
