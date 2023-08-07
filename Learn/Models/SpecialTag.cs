using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Learn.Models
{
    public partial class SpecialTag
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Special Tag")]
        public string Name { get; set; }
    }
}
