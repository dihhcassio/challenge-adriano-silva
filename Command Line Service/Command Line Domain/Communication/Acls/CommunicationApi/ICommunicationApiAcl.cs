using Command_Line_Domain.Communication.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Command_Line_Domain.Communication.Acls.CommunicationApi
{
    public interface ICommunicationApiAcl
    {

        Task<List<CommandDto>> GetCommands(string macAddress);
        Task SendReturn(CommandReturnSaveDto commandReturn);

        Task RegistryService(RegistryServiceDto registryServiceDto);

        Task NotifyOnline(NotifyOnlineDto notifyOnlineDto);
    }
}