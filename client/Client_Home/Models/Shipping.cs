using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class Shipping
{
    public int ShippingId { get; set; }

    public DateTime? ExpectedDate { get; set; }

    public string? TimeSlot { get; set; }

    public DateTime? CompleteTime { get; set; }

    public decimal? ShippingCost { get; set; }

    public string? ShipperName { get; set; }

    public int? LocationId { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ShippingLocation? Location { get; set; }
}
