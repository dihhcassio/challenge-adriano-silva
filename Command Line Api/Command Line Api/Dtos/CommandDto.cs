using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Command_Line_Api.Dtos
{
    public class CommandDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public string Result { get; set; }
    }
}
