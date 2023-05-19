using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Command_Line_Api.Dtos
{
    public class ClientDto
    {
        public string MacAddress { get; set; }
        public string HostName { get; set; }
        public string IpAddress { get; set; }
        public string AntivirusList { get; set; }
        public bool IsFirewallActive { get; set; }
        public string OsVersion { get; set; }
        public string DotNetVersion { get; set; }
        public List<HardDriveDto> HardDrives { get; set; }
    }
}
