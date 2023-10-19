using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class SellPriceHistory
{
    public int PriceId { get; set; }

    public int? ProductId { get; set; }

    public decimal? Sellprice { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Product? Product { get; set; }
}
