using Command_Line_Api_Domain.CommandLine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Domain.CommandLine.Services
{
    public interface IClientService
    {
        void Register(Client client);
        List<Client> GetOnlines();

        void NotifyOnline(string macAddress);
    }
}
