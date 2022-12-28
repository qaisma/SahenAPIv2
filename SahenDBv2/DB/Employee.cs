using System;
using System.Collections.Generic;

namespace SahenDBv2.DB
{
    public partial class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Guid RestaurantId { get; set; }
        public int UserType { get; set; }
        public bool IsAdmin { get; set; }

        public virtual Restaurant Restaurant { get; set; } = null!;
        public virtual EmployeeType UserTypeNavigation { get; set; } = null!;
    }
}
