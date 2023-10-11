using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public string? MethodName { get; set; }

    public string? Details { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
