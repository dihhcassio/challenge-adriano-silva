using Command_Line_Api.Dtos;
using Command_Line_Api_Domain.CommandLine.Dtos;
using Command_Line_Api_Domain.CommandLine.Models;
using Command_Line_Api_Domain.CommandLine.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Command_Line_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientService _clientService;

        public ClientController(ILogger<ClientController> logger, IClientService clientService)
        {
            _logger = logger;
            _clientService = clientService;
        }

        [HttpGet("onlines")]
        public List<ClientDto> GetOnlines()
        {
            var clients = _clientService.GetOnlines();

            return clients.Select(c => new ClientDto() 
            { 
                AntivirusList =  c.AntivirusList, 
                DotNetVersion = c.DotNetVersion, 
                HostName = c.HostName, 
                MacAddress = c.MacAddress, 
                IpAddress = c.IpAddress, 
                IsFirewallActive = c.IsFirewallActive, 
                OsVersion = c.OsVersion, 
                HardDrives = c.HardDrives?.Select(hd => new HardDriveDto() { Name = hd.Name, TotalFreeSpace = hd.TotalFreeSpace, TotalSize = hd.TotalSize }).ToList()
            }).ToList();
        }

        [HttpPost("register")]
        public IActionResult Register(ClientRegistryDtos clientRegistryDtos)
        {
            _clientService.Register(new Client()
            {
                AntivirusList = clientRegistryDtos.AntivirusList,
                DotNetVersion = clientRegistryDtos.DotNetVersion,
                HardDrives = clientRegistryDtos.HardDrives?.Select(hd => new HardDrive() { Name = hd.Name, TotalFreeSpace = hd.TotalFreeSpace, TotalSize = hd.TotalSize }).ToList(),
                HostName = clientRegistryDtos.HostName,
                IpAddress = clientRegistryDtos.IpAddress,
                IsFirewallActive = clientRegistryDtos.IsFirewallActive,
                MacAddress = clientRegistryDtos.MacAddress,
                OsVersion = clientRegistryDtos.OsVersion
            });

            return new OkResult();
        }

        [HttpPut("notify")]
        public IActionResult NotifyOnline(NotifyOnlineDto notifyOnlineDto)
        {

            _clientService.NotifyOnline(notifyOnlineDto.MacAddress);
            
            return new OkResult();
        }

    }
}
