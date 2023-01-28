using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Learn.Models
{
    public partial class ProductType
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Product Type")]
        public string ProductTypes { get; set; }
    }
}
