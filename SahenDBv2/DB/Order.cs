using System;
using System.Collections.Generic;

namespace SahenDBv2.DB
{
    public partial class Order
    {
        public Order()
        {
            OrdersMenuItems = new HashSet<OrdersMenuItem>();
        }

        public Guid Id { get; set; }
        public Guid VisitId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }

        public virtual OrdersStatus StatusNavigation { get; set; } = null!;
        public virtual Visit Visit { get; set; } = null!;
        public virtual ICollection<OrdersMenuItem> OrdersMenuItems { get; set; }
    }
}
