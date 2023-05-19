using Command_Line_Api_Domain.CommandLine.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Command_Line_Api_Domain.CommandLine.Repositories
{
    public interface IClientRepository
    {
        void Insert(Client client);
        void Update(Client client);
        Client FindOne(string macAddress);
        List<Client> FindByName(string clientName);
        List<Client> GetAll();
    }
}
