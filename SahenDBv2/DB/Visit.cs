using System;
using System.Collections.Generic;

namespace SahenDBv2.DB
{
    public partial class Visit
    {
        public Visit()
        {
            Orders = new HashSet<Order>();
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public Guid TableId { get; set; }
        public DateTime? EndedAt { get; set; }
        public int? Ranking { get; set; }

        public virtual RestaurantTable Table { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
