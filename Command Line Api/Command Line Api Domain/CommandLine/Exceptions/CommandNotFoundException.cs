using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Domain.CommandLine.Exceptions
{
    public class CommandNotFoundException: Exception
    {
        public CommandNotFoundException() : base("Command not found") { }
    }
}
