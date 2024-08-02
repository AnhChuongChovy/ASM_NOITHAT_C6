using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API_NoiThat.Models
{
    public class Shipping
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string PhuongThucVanChuyen { get; set; }
        public ICollection<Bill> Bill { get; set; }
    }
}
