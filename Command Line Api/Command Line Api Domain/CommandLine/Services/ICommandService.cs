using Command_Line_Api_Domain.CommandLine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Domain.CommandLine.Services
{
    public interface ICommandService
    {
        List<Command> SendCommand(String command, List<string> clientsNames);
        List<Command> GetCommandNotExecuted(string clientMacAddress);
        Command GetCommand(int idCommand);
        List<Command> GetLastCommands(int countCommands);
    }
}
