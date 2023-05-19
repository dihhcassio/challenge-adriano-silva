using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command_Line_Domain.WindowsInformations.Models
{
    public class HardDriveInformation
    {
        public long TotalFreeSpace { get; set; }
        public long TotalSize { get; set; }

        public string Name { get; set; }
    }
}
