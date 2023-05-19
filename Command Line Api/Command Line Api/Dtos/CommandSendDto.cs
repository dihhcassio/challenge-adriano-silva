using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Command_Line_Api.Dtos
{
    public class CommandSendDto
    {
        public string Command { get; set; }
        public List<string> ClientsNames { get; set; }
    }
}
