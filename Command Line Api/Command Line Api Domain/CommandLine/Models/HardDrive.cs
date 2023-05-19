using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Domain.CommandLine.Models
{
    public class HardDrive
    {
        public int Id { get; set; }
        public long TotalFreeSpace { get; set; }
        public long TotalSize { get; set; }
        public string Name { get; set; }
        public string MacAddressClient { get; set; }
        public Client Client { get; set; }
    }
}
