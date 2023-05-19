using Command_Line_Api.Dtos;
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
    public class CommandController
    {
        private readonly ILogger<CommandController> _logger;
        private readonly ICommandService _commandService;

        public CommandController(ILogger<CommandController> logger, ICommandService commandService)
        {
            _logger = logger;
            _commandService = commandService;
        }

        [HttpGet("not-executed/{macAddress}")]
        public IActionResult GetCommandNotExecuted(string macAddress)
        {
            _logger.LogInformation($"Recived request Command Not Executed from {macAddress}");
            var commands = _commandService.GetCommandNotExecuted(macAddress);
            return new OkObjectResult(commands.Select(c => new CommandDto()
            {
                Id = c.Id,
                Content = c.Content,
                Date = c.CreatedAt
            }));
        }

        [HttpPost("send")]
        public IActionResult SendCommand(CommandSendDto commandSendDto)
        {
            _logger.LogInformation($"Recived request Send Command {commandSendDto.Command} to {string.Join(",", commandSendDto.ClientsNames)}");

            var commands = _commandService.SendCommand(commandSendDto.Command, commandSendDto.ClientsNames);

            return new OkObjectResult(commands.Select(c => new CommandDto() 
            {
                Content = c.Content, 
                Date = c.CreatedAt, 
                Id = c.Id, 
                Result = c.CommandReturn?.Content
                }
            ));
        }

        [HttpGet("{idCommand}")]
        public IActionResult GetCommand(int idCommand)
        {
            _logger.LogInformation($"Recived request GetCommand {idCommand}");

            var command = _commandService.GetCommand(idCommand);

            return new OkObjectResult(new CommandDto()
            {
                Content = command.Content,
                Date = command.CreatedAt,
                Id = command.Id,
                Result = command.CommandReturn?.Content
            });
        }

        [HttpGet("/last/{countCommands}")]
        public IActionResult GetLastCount(int countCommands)
        {
            _logger.LogInformation($"Recived request GetCommand {countCommands}");

            var commands = _commandService.GetLastCommands(countCommands);

            return new OkObjectResult(commands.Select(c => new CommandDto()
            {
                Content = c.Content,
                Date = c.CreatedAt,
                Id = c.Id,
                Result = c.CommandReturn?.Content
            }
            ));
        }
    }
}
