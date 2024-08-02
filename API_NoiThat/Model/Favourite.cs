using System.ComponentModel.DataAnnotations;

namespace WebAppBlazor.Models
{
    public class Favourite
    {
        [Required]
        public int ID { get; set; }
        public int IDNguoiDung { get; set; }
        public int IDSanPham { get; set; }
        public Account Account { get; set; }
        public Product Product { get; set; }
    }
}
