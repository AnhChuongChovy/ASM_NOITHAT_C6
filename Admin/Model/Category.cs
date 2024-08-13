using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admin.Model
{
    public class Category
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string TenDanhMuc { get; set; }
        public string HinhAnh { get; set; }
        public string TrangThai { get; set; }
        public ICollection<CategoryType> CategoryType { get; set; }
        //public ICollection<Voucher> Voucher { get; set; }

    }
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
    }



    public class UploadImageResponse
    {
        public string FileUrl { get; set; }
    }
}
