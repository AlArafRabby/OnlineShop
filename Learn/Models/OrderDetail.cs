using System;
using System.Collections.Generic;

namespace Learn.Models
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PorductId { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Porduct { get; set; } = null!;
    }
}
