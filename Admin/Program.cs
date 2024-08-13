using Admin;
using Admin.Service;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
//builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44320/api/Product") });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44320/api/Account") });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44320/api/Login") });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44320/api/HoaDon") });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44320/api/Category") });

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<LocalStorageService>();
//builder.Services.AddScoped<AdminService>();

//builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddHttpClient("https://localhost:44320/api/", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

await builder.Build().RunAsync();
