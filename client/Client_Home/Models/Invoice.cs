using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int? CustomerId { get; set; }

    public int? EmployeeId { get; set; }

    public int? PaymentId { get; set; }

    public int? ShippingId { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual Payment? Payment { get; set; }

    public virtual Shipping? Shipping { get; set; }
}
