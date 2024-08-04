using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAsemly_NoiThat.Model;


namespace WebAsemly_NoiThat.Service
{
    public class LoginService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public LoginService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task<bool> IsLoggedIn()
        {
            var user = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "user");
            return !string.IsNullOrEmpty(user);
        }

        
    }
}
