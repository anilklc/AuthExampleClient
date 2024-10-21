﻿using AuthExampleClient.DTOs.Login;
using AuthExampleClient.DTOs.Token;
using AuthExampleClient.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace AuthExampleClient.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> AuthenticateAsync(string endpoint ,Login login)
        {
            var client = _httpClientFactory.CreateClient("AuthExampleApi");
            var response = await client.PostAsJsonAsync(endpoint,login);
            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(jsonContent);
                var objectArray = jsonObject["token"];
                var token = objectArray?.ToObject<Token>();
                SetTokenInCookie(token);
                return true;
            }

            return false;
        }

        public void RemoveTokenFromCookie()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("AccesToken");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("RefreshToken");
        }

        public void SetTokenInCookie(Token token)
        {
            var cookie = new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddHours(5),
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append("AccesToken",token.AccessToken ,cookie);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("RefreshToken", token.RefreshToken, cookie);

        }
    }
}