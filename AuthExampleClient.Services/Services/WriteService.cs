using AuthExampleClient.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AuthExampleClient.Services.Services
{
    public class WriteService<TEntity> : IWriteService<TEntity>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public WriteService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> CreateAsync(string endpoint, TEntity entity)
        {
            var client =  _httpClientFactory.CreateClient("AuthExampleApi");
            var response = await client.PostAsJsonAsync(endpoint,entity);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string endpoint, string id)
        {
            var client = _httpClientFactory.CreateClient("AuthExampleApi");
            var response = await client.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(string endpoint, TEntity entity)
        {
            var client = _httpClientFactory.CreateClient("AuthExampleApi");
            var response = await client.PutAsJsonAsync(endpoint, entity);
            return response.IsSuccessStatusCode;
        }
    }
}
