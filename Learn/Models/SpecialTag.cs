using System;
using System.Collections.Generic;

namespace Learn.Models
{
    public partial class SpecialTag
    {
        public SpecialTag()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
