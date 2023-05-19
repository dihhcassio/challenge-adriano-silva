using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command_Line_Domain.Communication.Services
{
    public interface ICommunicationService
    {
        Task ExecuteCommands();

        Task RegisterCommandLineService();

        Task NotifyOnline();
    }
}
