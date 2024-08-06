using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.JSInterop;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using WebAsemly_NoiThat.Service;

namespace WebAsemly_NoiThat.Pages
{
    public partial class GioHang
    {
        private List<Model.Product> cartItems = new List<Model.Product>();
        private double totalPrice = 0;
        private double vanchuyen = 1; // Bạn có thể thay đổi giá vận chuyển tùy theo nhu cầu
        
        protected override async Task OnInitializedAsync()
        {
            var userId = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "userId");

            if (!string.IsNullOrEmpty(userId))
            {
                // Lấy giỏ hàng theo ID người dùng từ Local Storage
                var cartJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", $"Cart_{userId}");
                cartItems = !string.IsNullOrEmpty(cartJson)
                    ? JsonSerializer.Deserialize<List<Model.Product>>(cartJson)
                    : new List<Model.Product>();
            }
            else
            {
                // Nếu chưa đăng nhập, lấy giỏ hàng từ Local Storage với tên mặc định "Cart"
                var cartJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "Cart");
                cartItems = !string.IsNullOrEmpty(cartJson)
                    ? JsonSerializer.Deserialize<List<Model.Product>>(cartJson)
                    : new List<Model.Product>();
            }

            // Tính tổng giá
            CalculateTotalPrice();
        }

        private void CalculateTotalPrice()
        {
            totalPrice = cartItems.Sum(item => item.Gia * item.Quantity) * vanchuyen;
        }

        private void ChuyenTrangThanhToan()
        {
            Navigation.NavigateTo("/ThanhToan");
        }

        private async Task UpdateQuantity(Model.Product item, int change)
        {
            item.Quantity = Math.Max(1, item.Quantity + change);

            // Lưu lại giỏ hàng vào Local Storage
            var userId = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "userId");
            if (!string.IsNullOrEmpty(userId))
            {
                var cartJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", $"Cart_{userId}");
                var cart = !string.IsNullOrEmpty(cartJson)
                    ? JsonSerializer.Deserialize<List<Model.Product>>(cartJson)
                    : new List<Model.Product>();

                var existingItem = cart.FirstOrDefault(p => p.ID == item.ID);
                if (existingItem != null)
                {
                    existingItem.Quantity = item.Quantity;
                }

                await JSRuntime.InvokeVoidAsync("localStorage.setItem", $"Cart_{userId}", JsonSerializer.Serialize(cart));
            }

            CalculateTotalPrice();
        }

        private async Task RemoveFromCart(Model.Product item)
        {
            cartItems.Remove(item);

            // Lưu lại giỏ hàng vào Local Storage
            var userId = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "userId");
            if (!string.IsNullOrEmpty(userId))
            {
                await JSRuntime.InvokeVoidAsync("localStorage.setItem", $"Cart_{userId}", JsonSerializer.Serialize(cartItems));
            }

            CalculateTotalPrice();
        }
    }
}
