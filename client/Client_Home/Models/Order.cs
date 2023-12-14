using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int SupplierId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Supplier Supplier { get; set; } = null!;
}
