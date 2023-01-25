using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Learn.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        //public int Id { get; set; }
        //public string Name { get; set; } = null!;
        //public decimal Price { get; set; }
        //public string? Image { get; set; }
        //public string? ProductColor { get; set; }
        //public bool IsAvailable { get; set; }
        //public int ProductTypeId { get; set; }
        //public int SpecialTagId { get; set; }

        //public virtual ProductType ProductType { get; set; }
        //public virtual SpecialTag SpecialTag { get; set; }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Image { get; set; }
        [Display(Name = "Product Color")]
        public string ProductColor { get; set; }
        [Required]
        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Product Type")]
        [Required]
        public int ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public virtual ProductType ProductType { get; set; }
        [Display(Name = "Special Tag")]
        [Required]
        public int SpecialTagId { get; set; }
        [ForeignKey("SpecialTagId")]
        public virtual SpecialTag SpecialTag { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
