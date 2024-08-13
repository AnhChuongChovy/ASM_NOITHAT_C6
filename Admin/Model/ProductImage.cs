using System.ComponentModel.DataAnnotations;
using Admin.Model;

namespace Admin.Model
{
    public class ProductImage
    {
        [Required]
        public int ID { get; set; }
        public int IDSanPham { get; set; }
        public string HinhAnh { get; set; }
        public Product Product { get; set; }
    }
}
