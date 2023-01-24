using System;
using System.Collections.Generic;

namespace Learn.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public string? ProductColor { get; set; }
        public bool IsAvailable { get; set; }
        public int ProductTypeId { get; set; }
        public int SpecialTagId { get; set; }

        public virtual ProductType ProductType { get; set; } = null!;
        public virtual SpecialTag SpecialTag { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
