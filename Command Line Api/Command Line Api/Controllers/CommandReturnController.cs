using Command_Line_Api.Dtos;
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
    public class CommandReturnController
    {
        private readonly ILogger<CommandReturnController> _logger;
        private readonly ICommandReturnService _commandReturnService;

        public CommandReturnController(ILogger<CommandReturnController> logger, ICommandReturnService commandReturnService)
        {
            _logger = logger;
            _commandReturnService = commandReturnService;
        }

        [HttpPost]
        public IActionResult Save(CommandReturnSaveDto commandReturnSaveDto)
        {
            _logger.LogInformation($"Save command return of {commandReturnSaveDto.CommandId} command");
            _commandReturnService.InsertCommandReturn(commandReturnSaveDto.CommandId, commandReturnSaveDto.Content);
            return new OkResult();
        }
    }
}
