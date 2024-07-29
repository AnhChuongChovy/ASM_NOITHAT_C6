using System.ComponentModel.DataAnnotations;
using WebAsemly_NoiThat.Model;

namespace WebAppBlazor.Models
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
