using System;
using System.Collections.Generic;

namespace SahenDBv2.DB
{
    public partial class OrdersMenuItem
    {
        public Guid OrderId { get; set; }
        public Guid MenuItemId { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; }

        public virtual MenuItem MenuItem { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
