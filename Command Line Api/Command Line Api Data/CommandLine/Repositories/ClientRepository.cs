using Command_Line_Api_Data.Configurations;
using Command_Line_Api_Domain.CommandLine.Models;
using Command_Line_Api_Domain.CommandLine.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command_Line_Api_Data.CommandLine.Repositories
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext context) : base(context) { }

        public List<Client> FindByName(string clientName)
        {
            return DbSet
               .Where(c => !c.Removed && c.HostName == clientName)
               .ToList();
        }

        public Client FindOne(string macAddress)
        {
            return DbSet
                .Include(c => c.HardDrives)
                .Where(c => !c.Removed && c.MacAddress == macAddress)
                .FirstOrDefault();
        }

        public List<Client> GetAll()
        {
            return DbSet.Include(c => c.HardDrives)
                .Where(c => !c.Removed)
                .ToList();
        }

        public void Insert(Client client)
        {
            client.CreatedAt = DateTime.Now;
            DbSet.Add(client);
            this.SaveChanges();
        }
        public void Update(Client client)
        {
            client.UpdatedAt = DateTime.Now;
            DbSet.Update(client);
            this.SaveChanges();
        }
    }
}
