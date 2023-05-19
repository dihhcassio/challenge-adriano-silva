using Command_Line_Api_Domain.CommandLine.Models;
using Command_Line_Api_Domain.CommandLine.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Data.CommandLine.Repositories
{
    public class CommandReturnRepository : RepositoryBase<CommandReturn>, ICommandReturnRepository
    {
        public CommandReturnRepository(ApplicationDbContext context) : base(context) { }

        public void Insert(CommandReturn commandReturn)
        {
            DbSet.Add(commandReturn);
            this.SaveChanges();
        }
    }
}
