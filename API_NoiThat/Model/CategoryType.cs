using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppBlazor.Models
{
    public class CategoryType
    {
        [Required]
        public int ID { get; set; }
        public int IDDanhMuc { get; set; }
        public string TenLoaiDanhMuc { get; set; }
        public string HinhAnh { get; set; }
        public Category Category { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
