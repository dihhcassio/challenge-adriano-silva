using Command_Line_Api_Data.CommandLine.Mapping;
using Command_Line_Api_Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Data.CommandLine.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        public string DbPath { get; private set; }

        public ApplicationDbContext(DataConfig dataConfig)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}{dataConfig.DbName}";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
            options.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClientMapping());
            modelBuilder.ApplyConfiguration(new HardDriveMapping());
            modelBuilder.ApplyConfiguration(new CommandMapping());
            modelBuilder.ApplyConfiguration(new CommandReturnMapping());
        }
    }
}
