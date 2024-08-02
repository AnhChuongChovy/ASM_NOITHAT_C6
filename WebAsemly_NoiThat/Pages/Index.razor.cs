using Microsoft.JSInterop;
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
            // Lấy dữ liệu giỏ hàng từ localStorage
            var existingCart = await LocalStorageService.GetItemAsync<List<Model.Product>>("Cart");
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
            await LocalStorageService.SetItemAsync("Cart", cart);

            // Cung cấp phản hồi cho người dùng
            await JSRuntime.InvokeVoidAsync("alert", "Sản phẩm đã được thêm vào giỏ hàng!");
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
