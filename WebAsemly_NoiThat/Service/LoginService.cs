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

        //public async Task<bool> LoginAsync(UserLoginModel model)
        //{
        //    var response = await _httpClient.PostAsJsonAsync("https://localhost:44320/api/Login/", model);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var account = await response.Content.ReadFromJsonAsync<Account>();

        //        // Lưu thông tin vào localStorage
        //        await _jsRuntime.InvokeVoidAsync("localStorageHelper.setItem", "authToken", account.TenNguoiDung);
        //        await _jsRuntime.InvokeVoidAsync("localStorageHelper.setItem", "userId", account.ID.ToString());
        //        await _jsRuntime.InvokeVoidAsync("localStorageHelper.setItem", "userRole", account.Role.NameRole);

        //        return true;
        //    }
        //    return false;
        //}
    }
}
