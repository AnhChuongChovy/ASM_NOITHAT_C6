﻿using System.Collections;
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
        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        public string MatKhau { get; set; }
        [Required]
        public string DiaChi { get; set; }
        [Required]
        public string SDT { get; set; }
        [Required]
        public string GioiTinh { get; set; }
        public Role Role { get; set; }
        //public ICollection<Bill> Bill { get; set; }
        //public ICollection<Favourite> Favourite { get; set; }
        //public ICollection<Reviews> Reviews { get; set; }

    }
    public class LoginRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
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
