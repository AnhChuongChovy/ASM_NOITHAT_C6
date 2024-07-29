using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppBlazor.Models
{
    public class Payment
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string PhuongThucThanhToan { get; set; }
        public ICollection<Bill> Bill { get; set; }
    }
}
