using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Command_Line_Api.Dtos
{
    public class RegistryHardDriveDto
    {
        public long TotalFreeSpace { get; set; }
        public long TotalSize { get; set; }

        public string Name { get; set; }
    }
}
