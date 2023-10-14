using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? SellPrice { get; set; }

    public int TotalQuantity { get; set; }

    public int? CategoryId { get; set; }

    public string? ThumbnailUrl { get; set; }

    public string? VideoUrl { get; set; }

    public decimal? Discount { get; set; }

    public decimal? DiscountPrice { get; set; }

    public bool? BestsellerFlag { get; set; }

    public bool? HomeFlag { get; set; }

    public bool? Active { get; set; }

    public int? SupplierId { get; set; }

    public DateTime? DateAdded { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual ICollection<ProductBatch> ProductBatches { get; set; } = new List<ProductBatch>();

    public virtual ICollection<ProductSubImage> ProductSubImages { get; set; } = new List<ProductSubImage>();

    public virtual Supplier? Supplier { get; set; }
}
