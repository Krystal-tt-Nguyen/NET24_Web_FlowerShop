using System;
using System.Collections.Generic;

namespace Webshop.Entities;

public partial class Customer
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? StreetAddress { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
