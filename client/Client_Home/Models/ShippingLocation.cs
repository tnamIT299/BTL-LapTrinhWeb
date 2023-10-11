using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class ShippingLocation
{
    public int LocationId { get; set; }

    public int? CustomerId { get; set; }

    public string? Street { get; set; }

    public string? Ward { get; set; }

    public string? District { get; set; }

    public string? City { get; set; }

    public bool? IsDefault { get; set; }

    public virtual Customer? Customer { get; set; }
}
