using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Platform.API.SyncDataServices.Http
{
    using Platform.API.Infrastructure.Dtos;

    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendPlatform(PlatformModelReadDto platform)
        {
            var payload = new StringContent(JsonSerializer.Serialize(platform), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_configuration["COMMAND_API"]}", payload);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Post request to command api is success");
            }
            else
            {
                Console.WriteLine("--> Post request to command api is unsuccess");
            }
        }
    }
}
