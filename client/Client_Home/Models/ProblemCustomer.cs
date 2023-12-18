using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class ProblemCustomer
{
    public int ProblemId { get; set; }

    public int? CustomerId { get; set; }

    public string? Subject { get; set; }

    public string? Message { get; set; }

    public int? Status { get; set; }
}
