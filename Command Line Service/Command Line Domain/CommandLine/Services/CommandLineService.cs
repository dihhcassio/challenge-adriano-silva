using Command_Line_Domain.CommandLine.CommandExecuter;
using Command_Line_Domain.CommandLine.Execptions;
using Microsoft.Extensions.Logging;
using System;

namespace Command_Line_Domain.CommandLine.Services
{
    public class CommandLineService : ICommandLineService
    {
        private readonly ILogger<CommandLineService> _logger;
        private readonly ICommandExecuter _commanderExecuter;

        public CommandLineService(ILogger<CommandLineService> logger, ICommandExecuter commandExecuter)
        {
            _logger = logger;
            _commanderExecuter = commandExecuter;
        }

        public string RunCommand(string command)
        {
            if (string.IsNullOrEmpty(command)) throw new EmptyCommandException();

            return _commanderExecuter.Execute(command);
        }
    }
}
