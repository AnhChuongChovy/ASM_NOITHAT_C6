using System.ComponentModel.DataAnnotations;

namespace WebAsemly_NoiThat.Model
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu hiện tại.")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới.")]
        [StringLength(100, ErrorMessage = "Mật khẩu mới phải có ít nhất {2} ký tự.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu mới.")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu mới không khớp.")]
        public string ConfirmPassword { get; set; }
    }
}
