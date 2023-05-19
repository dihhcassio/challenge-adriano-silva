using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Command_Line_Domain.Utils
{
    public static class HttpResponseMessageExtension
    {
        public static async Task<T> DeserializeObjectAsync<T>(this HttpResponseMessage httpResponseMessage) 
        {
            var jsonString = await httpResponseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
