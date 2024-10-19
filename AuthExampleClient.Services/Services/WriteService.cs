using AuthExampleClient.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AuthExampleClient.Services.Services
{
    public class WriteService<TCreate,TUpdate> : IWriteService<TCreate,TUpdate>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient client;
        public WriteService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            client = _httpClientFactory.CreateClient("AuthExampleApi");
        }

        public async Task<bool> CreateAsync(string endpoint, TCreate entity)
        {
            var response = await client.PostAsJsonAsync(endpoint,entity);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string endpoint, string id)
        {
            var response = await client.DeleteAsync($"{endpoint}{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(string endpoint, TUpdate entity)
        {
            var response = await client.PutAsJsonAsync(endpoint, entity);
            return response.IsSuccessStatusCode;
        }
    }
}
