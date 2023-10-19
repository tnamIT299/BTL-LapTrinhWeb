using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class Discount
{
    public int DiscountId { get; set; }

    public int? ProductId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? DiscountPercent { get; set; }

    public bool? IsActive { get; set; }

    public virtual Product? Product { get; set; }
}
