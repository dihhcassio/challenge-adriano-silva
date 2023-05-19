using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Domain.CommandLine.Models
{
    public class Command
    {
        public int Id { get; set; }

        public string Content { get; set; }
        public bool HasExecuted { get; set; }
        public Client Client { get; set; }

        public string MacAddressClient { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Removed { get; set; }
        public CommandReturn CommandReturn { get; set; }
    }
}
