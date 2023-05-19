using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command_Line_Domain.CommandLine.CommandExecuter
{
    public interface ICommandExecuter
    {
        public string Execute(string command);
    }
}
