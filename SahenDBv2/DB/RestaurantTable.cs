using System;
using System.Collections.Generic;

namespace SahenDBv2.DB
{
    public partial class RestaurantTable
    {
        public RestaurantTable()
        {
            Visits = new HashSet<Visit>();
        }

        public Guid Id { get; set; }
        public Guid RestaurantId { get; set; }
        public string? QrCode { get; set; }
        public string? Name { get; set; }
        public bool IsReserved { get; set; }

        public virtual Restaurant Restaurant { get; set; } = null!;
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
