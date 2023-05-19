using Command_Line_Api_Domain.CommandLine.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Data.CommandLine.Mapping
{
    public class ClientMapping : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("client");

            builder.HasKey(c => c.MacAddress);

            builder.Property(c => c.MacAddress).IsRequired();

            builder.Property(c => c.HostName);
            builder.Property(c => c.IpAddress);
            builder.Property(c => c.IsFirewallActive);
            builder.Property(c => c.LastNotify);
            builder.Property(c => c.OsVersion);
            builder.Property(c => c.AntivirusList);
            builder.Property(c => c.DotNetVersion);
            builder.Property(c => c.CreatedAt);
            builder.Property(c => c.UpdatedAt);
            builder.Property(c => c.Removed);

            builder.HasMany(c => c.HardDrives)
                .WithOne(h => h.Client);
            builder.HasMany(c => c.Commands)
               .WithOne(c => c.Client);
        }
    }
}
