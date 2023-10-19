using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class CartItem
{
    public int CartId { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Total { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
