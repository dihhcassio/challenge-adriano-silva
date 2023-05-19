using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Domain.CommandLine.Exceptions
{
    public class ClientNullException : Exception
    {
        public ClientNullException() : base("Cliet is null") { }
    }
}
