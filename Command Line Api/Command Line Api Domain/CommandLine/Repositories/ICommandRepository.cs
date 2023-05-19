using Command_Line_Api_Domain.CommandLine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Domain.CommandLine.Repositories
{
    public interface ICommandRepository
    {
        List<Command> GetCommandNotExecuted(string clientMacAddress);

        Command Insert(Command command);

        void Update(Command command);

        Command FindOne(int commandId);
        List<Command> FindLasts(int countCommands);
    }
}
