using System;
using System.Collections.Generic;

namespace SahenDBv2.DB
{
    public partial class Manager
    {
        public Manager()
        {
            Restaurants = new HashSet<Restaurant>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}
