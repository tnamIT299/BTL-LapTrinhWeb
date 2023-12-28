using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class SalesLead
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Source { get; set; } = null!;
}
