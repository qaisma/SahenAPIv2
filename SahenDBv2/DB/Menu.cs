using System;
using System.Collections.Generic;

namespace SahenDBv2.DB
{
    public partial class Menu
    {
        public Menu()
        {
            MenuItems = new HashSet<MenuItem>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsFeatured { get; set; }
        public Guid RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; } = null!;
        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}
