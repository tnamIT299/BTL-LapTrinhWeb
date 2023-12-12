using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public virtual Orders Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
