using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAsemly_NoiThat.Model;

namespace WebAsemly_NoiThat.Pages
{
    public partial class TaiKhoan
    {
        [Parameter]
        public int id { get; set; }
        private string Message;

        private Account account = new Account();
        private IBrowserFile selectedFile;

        // Load thông tin tài khoản theo id đã lưu vào local
        protected override async Task OnInitializedAsync()
        {
            var userId = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "userId");

            if (!string.IsNullOrEmpty(userId))
            {
                account = await Http.GetFromJsonAsync<Account>($"https://localhost:44320/api/Account/{id}");
            }
            else
            {
                Navigation.NavigateTo("/DangNhap");
            }
        }

        // Xử lý khi chọn tệp tin
        private async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;
            if (file != null)
            {
                var content = new MultipartFormDataContent();
                var streamContent = new StreamContent(file.OpenReadStream(maxAllowedSize: 1024 * 1024 * 15)); // 15 MB
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                content.Add(streamContent, "file", file.Name);

                var response = await Http.PostAsync("https://localhost:44320/api/Account/upload-image", content);

                if (response.IsSuccessStatusCode)
                {
                    // Sử dụng lớp dữ liệu UploadImageResponse
                    var result = await response.Content.ReadFromJsonAsync<UploadImageResponse>();
                    if (result != null)
                    {
                        var fileUrl = result.FileUrl;
                        account.HinhAnh = fileUrl; // Lưu URL hình ảnh vào thuộc tính của category
                        Console.WriteLine($"Hình ảnh đã được tải lên: {account.HinhAnh}");
                    }
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Lỗi khi tải lên hình ảnh: {errorMessage}");
                }
            }
        }

        // Cập nhật thông tin
        private async Task UpdateAccount()
        {
            var response = await Http.PutAsJsonAsync($"https://localhost:44320/api/Account/{account.ID}", account);

            if (response.IsSuccessStatusCode)
            {
                Message = "Cập nhật thành công!";
                StateHasChanged();

                // Đợi 3 giây rồi ẩn thông báo
                await Task.Delay(3000);
                Message = string.Empty;
                StateHasChanged();
            }
            else
            {
                Console.WriteLine("Cập nhật không thành công.");
            }
        }
    }
}
