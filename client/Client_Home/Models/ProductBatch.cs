using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class ProductBatch
{
    public int BatchId { get; set; }

    public int ProductId { get; set; }

    public string? BatchNumber { get; set; }

    public DateTime? ManufactureDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public int? Quantity { get; set; }

    public string? Barcode { get; set; }

    public virtual Product Product { get; set; } = null!;
}
