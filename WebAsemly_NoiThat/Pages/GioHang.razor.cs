using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace WebAsemly_NoiThat.Pages
{
    public partial class GioHang
    {
        private List<Model.Product> cartItems;
        private double totalPrice = 0;
        private double vanchuyen = 1;
        protected override async Task OnInitializedAsync()
        {
            cartItems = await LocalStorageService.GetItemAsync<List<Model.Product>>("Cart") ?? new List<Model.Product>();
            CalculateTotalPrice();
        }

        private void CalculateTotalPrice()
        {
            totalPrice = cartItems.Sum(item => item.Gia * item.Quantity * vanchuyen);
        }

        private void ChuyenTrangThanhToan()
        {
            Navigation.NavigateTo("/ThanhToan");
        }

        private async Task UpdateQuantity(Model.Product item, int change)
        {
            item.Quantity = Math.Max(1, item.Quantity + change);
            await LocalStorageService.SetItemAsync("Cart", cartItems);
            CalculateTotalPrice();
        }

        private async Task RemoveFromCart(Model.Product item)
        {
            cartItems.Remove(item);
            await LocalStorageService.SetItemAsync("Cart", cartItems);
            CalculateTotalPrice();
        }
    }
}
