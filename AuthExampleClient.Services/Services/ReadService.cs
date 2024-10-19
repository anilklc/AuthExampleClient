using AuthExampleClient.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace AuthExampleClient.Services.Services
{
    public class ReadService<TEntity> : IReadService<TEntity>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient client;
        public ReadService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            client = _httpClientFactory.CreateClient("AuthExampleApi");
        }

        public async Task<TEntity> GetAsync(string endpoint,string id)
        {
            var response = await client.GetAsync($"{endpoint}{id}");
            return await response.Content.ReadFromJsonAsync<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync(string endpoint,string objectName)
        {
            var response = await client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            if (string.IsNullOrEmpty(objectName))
            {
                return await response.Content.ReadFromJsonAsync<List<TEntity>>();
            }
            var jsonContent = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(jsonContent);
            var objectArray = jsonObject[objectName];
            return objectArray?.ToObject<List<TEntity>>() ?? new List<TEntity>();
        }
    }
}
