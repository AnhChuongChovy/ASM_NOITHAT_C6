using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAsemly_NoiThat.Model
{
    public class Account
    {
        [Required]
        public int ID { get; set; }
        public int IDRole { get; set; }
        [Required]
        public string TenNguoiDung { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string MatKhau { get; set; }
        
        public string DiaChi { get; set; }
        
        public string SDT { get; set; }
        
        public string GioiTinh { get; set; }
        public string HinhAnh { get; set; }

        public Role Role { get; set; }
        //public ICollection<Bill> Bill { get; set; }
        //public ICollection<Favourite> Favourite { get; set; }
        //public ICollection<Reviews> Reviews { get; set; }

    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class CartItem
    {
        public int ProductId { get; set; }
        public string TenSP { get; set; }
        public decimal Gia { get; set; }
        public int Quantity { get; set; }
        public string HinhAnh { get; set; }

        // Bạn có thể thêm các thuộc tính khác nếu cần
    }
}
