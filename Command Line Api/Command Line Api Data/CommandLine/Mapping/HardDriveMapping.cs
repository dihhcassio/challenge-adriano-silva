using Command_Line_Api_Domain.CommandLine.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Data.CommandLine.Mapping
{
    public class HardDriveMapping : IEntityTypeConfiguration<HardDrive>
    {
        public void Configure(EntityTypeBuilder<HardDrive> builder)
        {
            builder.ToTable("hard_drive");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.Id).IsRequired();

            builder.Property(h => h.Name);
            builder.Property(h => h.TotalFreeSpace);
            builder.Property(h => h.TotalSize);
            builder.Property(h => h.MacAddressClient).IsRequired();
            builder.HasOne<Client>(h => h.Client)
               .WithMany(c => c.HardDrives)
               .HasForeignKey(c => c.MacAddressClient);
        }

    }
}
