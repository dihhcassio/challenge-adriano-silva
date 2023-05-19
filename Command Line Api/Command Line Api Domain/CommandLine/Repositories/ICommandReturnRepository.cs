using Command_Line_Api_Domain.CommandLine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Domain.CommandLine.Repositories
{
    public interface ICommandReturnRepository
    {
        void Insert(CommandReturn commandReturn);
    }
}
