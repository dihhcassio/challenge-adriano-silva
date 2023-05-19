using Command_Line_Api_Domain.CommandLine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Domain.CommandLine.Services
{
    public interface ICommandReturnService
    {
        void InsertCommandReturn(int commandId, string content);
    }
}
