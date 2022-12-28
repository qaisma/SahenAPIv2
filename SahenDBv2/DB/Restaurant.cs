using System;
using System.Collections.Generic;

namespace SahenDBv2.DB
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            Employees = new HashSet<Employee>();
            Menus = new HashSet<Menu>();
            RestaurantTables = new HashSet<RestaurantTable>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid ManagerId { get; set; }
        public byte[]? Logo { get; set; }
        public decimal Tax { get; set; }
        public string CurrencyCode { get; set; } = null!;

        public virtual Manager Manager { get; set; } = null!;
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<RestaurantTable> RestaurantTables { get; set; }
    }
}
