using Command_Line_Api.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command_Line_Api_Domain.CommandLine.Dtos
{
    public class ClientRegistryDtos
    {
        public string HostName { get; set; }
        public string IpAddress { get; set; }
        public string MacAddress { get; set; }
        public string AntivirusList { get; set; }
        public bool IsFirewallActive { get; set; }
        public string OsVersion { get; set; }
        public string DotNetVersion { get; set; }
        public List<RegistryHardDriveDto> HardDrives { get; set; }
    }
}
