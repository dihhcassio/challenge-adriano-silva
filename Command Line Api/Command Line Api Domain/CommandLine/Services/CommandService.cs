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
    public class CommandService : ICommandService
    {
        private readonly ILogger<CommandService> _logger;
        private readonly ICommandRepository _commandResitory;
        private readonly IClientRepository _clientRepository;

        public CommandService(ILogger<CommandService> logger, ICommandRepository commandResitory, IClientRepository clientRepository) 
        {
            _logger = logger;
            _commandResitory = commandResitory;
            _clientRepository = clientRepository;
        }

        public List<Command> GetCommandNotExecuted(string clientMacAddress)
        {
            _logger.LogInformation($"Get all commands not executed to client {clientMacAddress}");
            var commands = _commandResitory.GetCommandNotExecuted(clientMacAddress);

            var dateTimeToLimiteCommandCanBeExecuted = DateTime.Now.AddMilliseconds(-10000);

            _logger.LogInformation("Filter commands created 3 seconds ago");
            return commands.Where(c => c.CreatedAt >= dateTimeToLimiteCommandCanBeExecuted).ToList();
        }

        public List<Command> SendCommand(string command, List<string> clientsNames)
        {
            if (clientsNames == null) throw new ClientsNameNullException();

            _logger.LogInformation($"Send command {command} to {string.Join(",", clientsNames)}");

            var commands = new List<Command>();
            foreach (string clientName in clientsNames) 
            {
                _logger.LogInformation($"Get client with name {clientName}");
                var clients = _clientRepository.FindByName(clientName);

                if (clients == null) throw new ListClientNullException();

                foreach (var client in clients) 
                {
                    _logger.LogInformation($"Save commands to {client.MacAddress} client mac address");
                    var commandObj = new Command()
                    {
                        Content = command,
                        HasExecuted = false, 
                        Client = client
                    };

                    commands.Add(_commandResitory.Insert(commandObj));

                }
            }
            return commands;
        }

        public List<Command> GetLastCommands(int countCommands)
        {
            _logger.LogInformation($"Get last  {countCommands} commands");

            return _commandResitory.FindLasts(countCommands);
        }

        public Command GetCommand(int idCommand)
        {
            _logger.LogInformation($"Get command {idCommand}");

            return _commandResitory.FindOne(idCommand);
        }
    }
}
