using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Domain.CommandLine.Exceptions
{
    public class ClientsNameNullException : Exception
    {
        public ClientsNameNullException() : base("Clients name list is null") { }
    }
}
