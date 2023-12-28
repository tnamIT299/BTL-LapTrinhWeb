using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class ProductBatch
{
    public int BatchId { get; set; }

    public int ProductId { get; set; }

    public DateTime? ManufactureDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public int? Quantity { get; set; }

    public string? Barcode { get; set; }

    public decimal? ImportPrice { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual Product Product { get; set; } = null!;
}
