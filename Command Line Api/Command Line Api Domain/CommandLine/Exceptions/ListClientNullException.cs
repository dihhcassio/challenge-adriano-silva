using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Domain.CommandLine.Exceptions
{
    public class ListClientNullException : Exception
    {
        public ListClientNullException() : base("List Client is null") { }
    }
}
