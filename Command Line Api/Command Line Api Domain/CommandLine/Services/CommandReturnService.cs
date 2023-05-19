using Command_Line_Api_Domain.CommandLine.Exceptions;
using Command_Line_Api_Domain.CommandLine.Models;
using Command_Line_Api_Domain.CommandLine.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Domain.CommandLine.Services
{
    public class CommandReturnService : ICommandReturnService
    {

        private readonly ILogger<CommandReturnService> _logger;
        private readonly ICommandReturnRepository _commandReturnRepository;
        private readonly ICommandRepository _commandRepository;

        public CommandReturnService(ILogger<CommandReturnService> logger, 
            ICommandReturnRepository commandReturnRepository,
            ICommandRepository commandRepository) 
        {
            _logger = logger;
            _commandReturnRepository = commandReturnRepository;
            _commandRepository = commandRepository;
        }

        public void InsertCommandReturn(int commandId, string content)
        {
            _logger.LogInformation($"Find command by id {commandId}");
            var command = _commandRepository.FindOne(commandId);

            if (command == null) throw new CommandNotFoundException();

            command.HasExecuted = true;

            var commandReturn = new CommandReturn()
            {
                Command = command,
                Content = content
            };

            _logger.LogInformation("Insert command return");
            _commandReturnRepository.Insert(commandReturn);
            _commandRepository.Update(command);
        }
    }
}
