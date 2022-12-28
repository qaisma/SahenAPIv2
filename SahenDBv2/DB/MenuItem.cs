using System;
using System.Collections.Generic;

namespace SahenDBv2.DB
{
    public partial class MenuItem
    {
        public MenuItem()
        {
            OrdersMenuItems = new HashSet<OrdersMenuItem>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public Guid MenuId { get; set; }
        public bool? IsEnabled { get; set; }

        public virtual Menu Menu { get; set; } = null!;
        public virtual ICollection<OrdersMenuItem> OrdersMenuItems { get; set; }
    }
}
