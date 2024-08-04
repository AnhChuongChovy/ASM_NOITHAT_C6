using System.ComponentModel.DataAnnotations;

namespace WebAsemly_NoiThat.Model
{
    public class ChangePasswordRequest
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Xác nhận mật khẩu không khớp.")]
        public string ConfirmNewPassword { get; set; }
    }
}
