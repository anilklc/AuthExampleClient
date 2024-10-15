using AuthExampleClient.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AuthExampleClient.Services.Services
{
    public class ReadService<TEntity> : IReadService<TEntity>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReadService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TEntity> GetAsync(string endpoint)
        {
            var client = _httpClientFactory.CreateClient("AuthExampleApi");
            var response = await client.GetAsync(endpoint);
            return await response.Content.ReadFromJsonAsync<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync(string endpoint)
        {
            var client = _httpClientFactory.CreateClient("AuthExampleApi");
            var response = await client.GetAsync(endpoint);
            return await response.Content.ReadFromJsonAsync<List<TEntity>>();
        }
    }
}
