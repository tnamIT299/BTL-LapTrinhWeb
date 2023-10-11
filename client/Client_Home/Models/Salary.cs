using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class Salary
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? MonthYear { get; set; }

    public int? HoursWorked { get; set; }

    public decimal? HourlyRate { get; set; }

    public decimal? Bonus { get; set; }

    public decimal? Deductions { get; set; }

    public decimal? Total { get; set; }

    public virtual Employee? Employee { get; set; }
}
