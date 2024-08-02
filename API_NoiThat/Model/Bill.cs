using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API_NoiThat.Models
{
    public class Bill
    {
        [Required]
        public int ID { get; set; }
        public int IDThanhToan { get; set; }
        public int IDVanChuyen { get; set; }
        public int IDNguoiDung { get; set; }
        [Required]
        public float TongTien { get; set; }
        [Required]
        public string NgayDatHang { get; set; }
        [Required]
        public string TrangThai { get; set; }
        public Account Account { get; set; }
        public Payment Payment { get; set; }
        public Shipping Shipping { get; set; }
        public ICollection<BillDetail> BillDetail { get; set; }
    }
}
