using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Domain.CommandLine.Models
{
    public class Client
    {
        public string MacAddress { get; set; }
        public string HostName { get; set; }
        public string IpAddress { get; set; }
        public string AntivirusList { get; set; }
        public bool IsFirewallActive { get; set; }
        public string OsVersion { get; set; }
        public string DotNetVersion { get; set; }
        public DateTime LastNotify { get; set; }

        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Removed { get; set; }


        public ICollection<HardDrive> HardDrives { get; set; }
        public ICollection<Command> Commands { get; set; }
    }
}
