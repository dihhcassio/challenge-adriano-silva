using Command_Line_Api_Data.CommandLine.Mapping;
using Command_Line_Api_Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;

namespace Command_Line_Api_Data.CommandLine.Repositories
{
    public class RepositoryBase<TEntity> where TEntity : class
    {
        protected DbSet<TEntity> DbSet { get; set; }
        protected ApplicationDbContext _context;

        public RepositoryBase(ApplicationDbContext context) 
        {
            _context = context;
            DbSet = context.Set<TEntity>();
        }
        protected void SaveChanges()
        {
            _context.SaveChanges();
        }



    }
}
