using System;
using System.ComponentModel.DataAnnotations;

namespace API_NoiThat.Models
{
    public class Voucher
    {
        [Required]
        public int ID { get; set; }
        public int IDDanhMuc { get; set; }
        public int IDSanPham { get; set; }
        [Required]
        public string TenVoucher { get; set; }
        [Required]
        public string LoaiVoucher { get; set; }
        [Required]
        public int SoLuong { get; set; }
        [Required]
        public string MoTa { get; set; }
        public string HinhAnh { get; set; }
        [Required]
        public DateTime NgayDang { get; set; }
        [Required]
        public DateTime NgayHetHan { get; set; }
        [Required]
        public double PhanTramGiamGia { get; set; }
        [Required]
        public double DieuKienGiamGia { get; set; }
        [Required]
        public string TrangThai { get; set; }
        public Category Category { get; set; }
        public Product Product { get; set; }


    }
}
