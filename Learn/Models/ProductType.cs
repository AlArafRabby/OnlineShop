using System;
using System.Collections.Generic;

namespace Learn.Models
{
    public partial class ProductType
    {
        public ProductType()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string ProductType1 { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
