using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command_Line_Domain.CommandLine.Execptions
{
    public class EmptyCommandException: Exception
    {
        public EmptyCommandException() : base("Command is empty") { }
    }
}
