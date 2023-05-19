using Command_Line_Api_Domain.CommandLine.Exceptions;
using Command_Line_Api_Domain.CommandLine.Models;
using Command_Line_Api_Domain.CommandLine.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Command_Line_Api_Domain.CommandLine.Services
{
    public class ClientService : IClientService
    {
        private readonly ILogger<ClientService> _logger;
        private readonly IClientRepository _clientRepository;

        public ClientService(ILogger<ClientService> logger, IClientRepository clientRepository)
        {
            _logger = logger;
            _clientRepository = clientRepository;
        }

        public List<Client> GetOnlines()
        {
            _logger.LogInformation("Get All clients");
            var clients = _clientRepository.GetAll();

            if (clients == null) throw new ListClientNullException();

            var dateTimeToLimiteClientOnline = DateTime.Now.AddMilliseconds(-100000);

            _logger.LogInformation("Filter all clients where was notify at 3 seconds ago");
            return clients.Where(c => c.LastNotify >= dateTimeToLimiteClientOnline).ToList();
        }

        public void NotifyOnline(string macAddress)
        {
            _logger.LogInformation($"Notify client {macAddress} is online");
            var client = _clientRepository.FindOne(macAddress);

            if (client == null) throw new ClientNullException();

            client.LastNotify = DateTime.Now;
            _clientRepository.Update(client);
        }

        public void Register(Client client)
        {
            _logger.LogInformation($"Register client {client.MacAddress}");
            var clientRegsitred = _clientRepository.FindOne(client.MacAddress);
            if (clientRegsitred == null)
            {
                client.CreatedAt = DateTime.Now;
                _clientRepository.Insert(client);
            }
            else
            {
                clientRegsitred.AntivirusList = client.AntivirusList;
                clientRegsitred.DotNetVersion = client.DotNetVersion;
                clientRegsitred.HostName = client.HostName;
                clientRegsitred.IpAddress = client.IpAddress;
                clientRegsitred.IsFirewallActive = client.IsFirewallActive;
                clientRegsitred.OsVersion = client.OsVersion;
                clientRegsitred.HardDrives = client.HardDrives?.Select(hd => new HardDrive() { Name = hd.Name, TotalFreeSpace = hd.TotalFreeSpace, TotalSize = hd.TotalSize }).ToList();
                clientRegsitred.CreatedAt = client.CreatedAt;
                clientRegsitred.UpdatedAt = DateTime.Now;
                _clientRepository.Update(clientRegsitred);
            };
        }

    }
}
