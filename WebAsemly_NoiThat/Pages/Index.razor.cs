using Microsoft.JSInterop;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAsemly_NoiThat.Pages
{
    public partial class Index
    {
        private List<Model.Product> SanPham;
        private int soluongHienThi = 8;

        protected override async Task OnInitializedAsync()
        {
            SanPham = await ProductService.GetProducts();
        }

        private async Task AddToCartAsync(Model.Product product)
        {
            try
            {
                if (product.SoLuongTrongKho == 0)
                {
                    await JSRuntime.InvokeVoidAsync("alert", "Số lượng phải lớn hơn 0 để thêm sản phẩm vào giỏ hàng.");
                    return; // Ngừng thực hiện nếu số lượng bằng 0
                }
                // Lấy userId từ localStorage
                var userId = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "userId");

                // Khóa để lưu trữ giỏ hàng trong Local Storage
                var cartKey = string.IsNullOrEmpty(userId) ? "Cart" : $"Cart_{userId}";

                // Lấy dữ liệu giỏ hàng từ localStorage
                var existingCart = await LocalStorageService.GetItemAsync<List<Model.Product>>(cartKey);
                List<Model.Product> cart = existingCart ?? new List<Model.Product>();

                // Kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
                var existingProduct = cart.FirstOrDefault(p => p.ID == product.ID);
                if (existingProduct != null)
                {
                    // Nếu có, tăng số lượng của sản phẩm lên 1
                    existingProduct.Quantity++;
                }
                else
                {
                    // Nếu chưa, thêm sản phẩm vào giỏ hàng với số lượng ban đầu là 1
                    product.Quantity = 1;
                    cart.Add(product);
                }

                // Serialize danh sách giỏ hàng thành JSON và lưu lại vào localStorage
                await LocalStorageService.SetItemAsync(cartKey, cart);

                // Cung cấp phản hồi cho người dùng
                await JSRuntime.InvokeVoidAsync("alert", "Sản phẩm đã được thêm vào giỏ hàng!");
            }
            catch(Exception ex) 
            {
                await JSRuntime.InvokeVoidAsync("alert", $"Đã xảy ra lỗi khi thêm sản phẩm vào giỏ hàng: {ex.Message}");

            }

        }


        //Chuyển qua trang chi tiết khi nhấn vào sản phẩm 
        private void ChuyenTrang(int id)
        {
            var product = SanPham.FirstOrDefault(p => p.ID == id);
            Navigation.NavigateTo($"/ChiTietSanPham/{id}");
        }

        //Thêm 8 sản phẩm nữa khi bấm vào nút xem thêm
        private void LoadMore()
        {
            soluongHienThi += 8;
        }
    }
}
