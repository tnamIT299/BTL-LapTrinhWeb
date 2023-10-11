using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class Shipping
{
    public int ShippingId { get; set; }

    public string? MethodName { get; set; }

    public string? Provider { get; set; }

    public decimal? Cost { get; set; }

    public int? EstimatedDeliveryTime { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
