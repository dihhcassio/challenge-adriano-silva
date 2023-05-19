using Command_Line_Api_Domain.CommandLine.Models;
using Command_Line_Api_Domain.CommandLine.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Command_Line_Api_Data.CommandLine.Repositories
{
    public class CommandRespository : RepositoryBase<Command>, ICommandRepository
    {
        public CommandRespository(ApplicationDbContext context) : base(context) { }

        public Command FindOne(int commandId)
        {
            return DbSet
                .Include(c => c.CommandReturn)
                .Where(c => c.Id == commandId && !c.Removed)
                .SingleOrDefault();
        }

        public List<Command> GetCommandNotExecuted(string clientMacAddress)
        {
            return DbSet.Where(c => c.MacAddressClient == clientMacAddress && !c.Removed && !c.HasExecuted).ToList();
        }

        public Command Insert(Command command)
        {
            command.CreatedAt = DateTime.Now;
            DbSet.Add(command);
            this.SaveChanges();
            return command;
        }

        public void Update(Command command)
        {
            command.UpdatedAt = DateTime.Now;
            DbSet.Update(command);
            this.SaveChanges();
        }

        public List<Command> FindLasts(int countCommands)
        {
            return DbSet.AsQueryable()
                .Include(c => c.CommandReturn)
                .OrderBy(c => c.CreatedAt)
                .Where(c => !c.Removed)
                .Take(countCommands)
                .ToList();
        }

    }
}
