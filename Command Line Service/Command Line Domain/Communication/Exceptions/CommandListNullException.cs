using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command_Line_Domain.Communication.Exceptions
{
    public class CommandListNullException: Exception
    {
        public CommandListNullException() : base("Command List Object Is Null") { }
    }
}
