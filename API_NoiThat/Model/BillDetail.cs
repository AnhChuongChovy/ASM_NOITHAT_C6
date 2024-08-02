using Microsoft.EntityFrameworkCore.Query.Internal;
using System.ComponentModel.DataAnnotations;

namespace API_NoiThat.Models
{
    public class BillDetail
    {
        [Required]
        public int ID { get; set; }
        public int IDHoaDon { get; set; }
        public int IDSanPham { get; set; }
        [Required]
        public int SoLuong { get; set; }
        public Bill Bill { get; set; }
        public Product Product { get; set; }
    }
}
