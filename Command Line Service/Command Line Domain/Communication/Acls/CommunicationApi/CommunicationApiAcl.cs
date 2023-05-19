using Command_Line_Domain.Communication.Acls.CommunicationApi.Configurations;
using Command_Line_Domain.Communication.Dtos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Command_Line_Domain.Utils;
using System.Net.Http.Headers;

namespace Command_Line_Domain.Communication.Acls.CommunicationApi
{
    public class CommunicationApiAcl : ICommunicationApiAcl
    {
        private readonly ILogger<CommunicationApiAcl> _logger;

        private readonly CommunicationApiConfig _communicationApiConfig;

        private HttpClient _client;
        public CommunicationApiAcl(ILogger<CommunicationApiAcl> logger, CommunicationApiConfig communicationApiConfig)
        {
            _logger = logger;
            _communicationApiConfig = communicationApiConfig;
            _client = new HttpClient();
        }

        public async Task<List<CommandDto>> GetCommands(string macAddress)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{_communicationApiConfig.Host}/Command/not-executed/{macAddress}");
                response.EnsureSuccessStatusCode();
                return await response.DeserializeObjectAsync<List<CommandDto>>();
            }
            catch (HttpRequestException e)
            {
                _logger.LogError("Error on GetCommands", e);
                throw;
            }
        }

        public async Task NotifyOnline(NotifyOnlineDto notifyOnlineDto)
        {

            try
            {
                var jsonContent = JsonConvert.SerializeObject(notifyOnlineDto);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await _client.PutAsync($"{_communicationApiConfig.Host}/Client/notify", contentString);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                _logger.LogError("Error on NotifyOnline", e);
                throw;
            }
        }

        public async Task RegistryService(RegistryServiceDto registryServiceDto)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(registryServiceDto);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await _client.PostAsync($"{_communicationApiConfig.Host}/Client/register", contentString);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                _logger.LogError("Error on RegistryService", e);
                throw;
            }
        }

        public async Task SendReturn(CommandReturnSaveDto commandReturn)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(commandReturn);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await _client.PostAsync($"{_communicationApiConfig.Host}/CommandReturn", contentString);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                _logger.LogError("Error on SendReturns", e);
                throw;
            }
        }
    }
}
