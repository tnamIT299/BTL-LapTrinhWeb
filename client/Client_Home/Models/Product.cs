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

    public int? Active { get; set; }

    public int? SupplierId { get; set; }

    public DateTime? DateAdded { get; set; }

    public string? Qrcode { get; set; }

    public string? Unit { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    public virtual ICollection<ProductBatch> ProductBatches { get; set; } = new List<ProductBatch>();

    public virtual ICollection<ProductComment> ProductComments { get; set; } = new List<ProductComment>();

    public virtual ICollection<ProductSubImage> ProductSubImages { get; set; } = new List<ProductSubImage>();

    public virtual ICollection<SellPriceHistory> SellPriceHistories { get; set; } = new List<SellPriceHistory>();

    public virtual Supplier? Supplier { get; set; }

    public virtual ICollection<Product> Product1s { get; set; } = new List<Product>();

    public virtual ICollection<Product> Product2s { get; set; } = new List<Product>();
}
