using Command_Line_Api_Domain.CommandLine.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Data.CommandLine.Mapping
{
    public class CommandMapping : IEntityTypeConfiguration<Command>
    {
        public void Configure(EntityTypeBuilder<Command> builder)
        {
            builder.ToTable("command");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).IsRequired();

            builder.Property(c => c.Content).IsRequired();
            builder.Property(c => c.HasExecuted).IsRequired();
            builder.Property(c => c.Removed).IsRequired();
            builder.Property(c => c.UpdatedAt);
            builder.Property(c => c.CreatedAt);
            builder.Property(c => c.UpdatedAt);
            builder.Property(c => c.MacAddressClient).IsRequired();

            builder.HasOne(c => c.Client)
                .WithMany(c => c.Commands)
                .HasForeignKey(c => c.MacAddressClient);

            builder.HasOne(c => c.CommandReturn)
                .WithOne(c => c.Command)
                .HasForeignKey<CommandReturn>(c => c.CommandId);

        }
    }
}
