using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateTime? Birthday { get; set; }

    public int? RewardPoints { get; set; }

    public int? Rank { get; set; }

    public int? Gender { get; set; }

    public string? UserId { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<ShippingLocation> ShippingLocations { get; set; } = new List<ShippingLocation>();

    public virtual AspNetUser? User { get; set; }
}
