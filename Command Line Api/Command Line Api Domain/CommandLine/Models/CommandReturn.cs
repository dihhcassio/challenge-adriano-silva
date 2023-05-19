using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Domain.CommandLine.Models
{
    public class CommandReturn
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int CommandId { get; set; }
        public Command Command { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Removed { get; set; }
    }
}
