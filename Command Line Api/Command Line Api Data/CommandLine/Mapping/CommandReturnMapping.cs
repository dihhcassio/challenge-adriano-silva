using Command_Line_Api_Domain.CommandLine.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Data.CommandLine.Mapping
{
    public class CommandReturnMapping : IEntityTypeConfiguration<CommandReturn>
    {
        public void Configure(EntityTypeBuilder<CommandReturn> builder)
        {
            builder.ToTable("command_return");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).IsRequired();

            builder.Property(c => c.CommandId);
            builder.Property(c => c.CreatedAt);
            builder.Property(c => c.UpdatedAt);
            builder.Property(c => c.Removed);

            builder.HasOne(c => c.Command)
                .WithOne(c => c.CommandReturn)
                .HasForeignKey<CommandReturn>(c => c.CommandId);                
                
        }
    }
}
