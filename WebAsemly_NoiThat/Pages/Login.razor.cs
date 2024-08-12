using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using WebAsemly_NoiThat.Model;
using System.Text.Json;
using System.Linq;

namespace WebAsemly_NoiThat.Pages
{
    public partial class Login
    {
        private LoginModel loginModel = new LoginModel();

        private string errorMessage;
        private async Task HandleLogin()
        {
            var response = await Http.PostAsJsonAsync("https://localhost:44320/api/Account/login", loginModel);

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<Account>();
                var userId = user.ID.ToString();

                // Lưu thông tin người dùng vào Local Storage
                var userJson = JsonSerializer.Serialize(user);
                await JSRuntime.InvokeVoidAsync("localStorage.setItem", "user", userJson);
                await JSRuntime.InvokeVoidAsync("localStorage.setItem", "userId", userId);

                // Lấy giỏ hàng tạm thời từ Local Storage
                var tempCartJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "Cart");
                var tempCart = !string.IsNullOrEmpty(tempCartJson)
                    ? JsonSerializer.Deserialize<List<Model.Product>>(tempCartJson)
                    : new List<Model.Product>();

                // Lấy giỏ hàng lưu theo ID người dùng từ Local Storage (nếu có)
                var userCartJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", $"Cart_{userId}");
                var userCart = !string.IsNullOrEmpty(userCartJson)
                    ? JsonSerializer.Deserialize<List<Model.Product>>(userCartJson)
                    : new List<Model.Product>();

                // Hợp nhất giỏ hàng tạm thời với giỏ hàng của người dùng
                foreach (var item in tempCart)
                {
                    var existingProduct = userCart.FirstOrDefault(p => p.ID == item.ID);
                    if (existingProduct != null)
                    {
                        existingProduct.Quantity += item.Quantity;
                    }
                    else
                    {
                        userCart.Add(item);
                    }
                }

                // Lưu giỏ hàng theo ID người dùng vào Local Storage
                await JSRuntime.InvokeVoidAsync("localStorage.setItem", $"Cart_{userId}", JsonSerializer.Serialize(userCart));

                // Xóa giỏ hàng tạm thời khỏi Local Storage
                await JSRuntime.InvokeVoidAsync("localStorage.removeItem", "Cart");

                // Điều hướng người dùng sau khi đăng nhập
                if (user.IDRole == 1)
                {
                    NavigationManager.NavigateTo("/admin/index");
                }
                else if (user.IDRole == 2)
                {
                    NavigationManager.NavigateTo($"/TaiKhoan/{user.ID}");
                }
                else
                {
                    NavigationManager.NavigateTo("/unauthorized");
                }
            }
            else
            {
                errorMessage = "Thông tin đăng nhập không đúng. Vui lòng kiểm tra lại.";
                Console.WriteLine("Đăng nhập không thành công.");
            }
        }
    }
}
