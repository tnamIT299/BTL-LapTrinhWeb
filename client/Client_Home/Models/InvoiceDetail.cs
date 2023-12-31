﻿using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class InvoiceDetail
{
    public int InvoiceDetailId { get; set; }

    public int? InvoiceId { get; set; }

    public int? ProductBatchId { get; set; }

    public int? Quantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? Price { get; set; }

    public virtual Invoice? Invoice { get; set; }

    public virtual ProductBatch? ProductBatch { get; set; }
}
