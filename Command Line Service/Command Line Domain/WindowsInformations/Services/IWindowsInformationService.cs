using Command_Line_Domain.WindowsInformations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command_Line_Domain.WindowsInformations.Services
{
    public interface IWindowsInformationService
    {
        WindowsInformation LoadWindowsInformations();
    }
}
