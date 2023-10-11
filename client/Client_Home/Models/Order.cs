using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int SupplierId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal? TotalAmount { get; set; }
}
