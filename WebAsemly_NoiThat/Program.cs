using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebAsemly_NoiThat.Service;

namespace WebAsemly_NoiThat
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44320/api/Product") });
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44320/api/Account") });
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44320/api/Login") });
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44320/api/HoaDon") });

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<LocalStorageService>();
            builder.Services.AddScoped<AdminService>();
            //builder.Services.AddScoped<LoginService>();

            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddHttpClient("https://localhost:44320/", client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            });

            await builder.Build().RunAsync();
        }
    }
}
