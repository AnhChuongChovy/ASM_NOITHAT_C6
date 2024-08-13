using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Admin.Model;

namespace Admin.Model
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;

        public CustomAuthenticationStateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:44320/api/Login/login");
            if (response.IsSuccessStatusCode)
            {
                var account = await response.Content.ReadFromJsonAsync<Account>();
                var identity = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, account.Email),
            new Claim(ClaimTypes.Role, account.Role.ToString()),
            new Claim("UserId", account.ID.ToString())
        }, "apiauth_type");

                var user = new ClaimsPrincipal(identity);
                return new AuthenticationState(user);
            }
            else
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

    }
}
