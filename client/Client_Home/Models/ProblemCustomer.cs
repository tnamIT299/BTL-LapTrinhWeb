using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Client_Home.Models;

public partial class ProblemCustomer
{
    [Key]
    public int ProblemId { get; set; }

    public int? CustomerId { get; set; }

    public string? Subject { get; set; }

    public string? Message { get; set; }

    public int? Status { get; set; }
}
