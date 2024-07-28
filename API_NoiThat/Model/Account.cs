using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppBlazor.Models
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
        [Required]
        public string DiaChi { get; set; }
        [Required]
        public string SDT { get; set; }
        [Required]
        public string GioiTinh { get; set; }
        public Role Role { get; set; }
        public ICollection<Bill> Bill { get; set; }
        public ICollection<Favourite> Favourite { get; set; }
        public ICollection<Reviews> Reviews { get; set; }

    }
}
