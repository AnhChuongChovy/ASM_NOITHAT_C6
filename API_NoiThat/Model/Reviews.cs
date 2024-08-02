using System.ComponentModel.DataAnnotations;

namespace API_NoiThat.Models
{
    public class Reviews
    {
        [Required]
        public int ID { get; set; }
        public int IDSanPham { get; set; }
        public int IDTaiKhoan { get; set; }
        public string NoiDung { get; set; }
        public double DanhGia { get; set; }
        public Product Product { get; set; }
        public Account Account { get; set; }
    }
}
