﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API_NoiThat.Models
{
    public class Role
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string NameRole { get; set; }
        public ICollection<Account> Account { get; set; }
    }
}
