using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppBlazor.Models
{
    public class Category
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string TenDanhMuc { get; set; }
        public string HinhAnh { get; set; }
        public ICollection<CategoryType> CategoryType { get; set; }
        //public ICollection<Voucher> Voucher { get; set; }

    }

}
