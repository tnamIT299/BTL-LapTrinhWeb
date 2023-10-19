using System;
using System.Collections.Generic;

namespace Client_Home.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int? UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Position { get; set; }

    public DateTime? DateHired { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? CitizenId { get; set; }

    public bool? Gender { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();

    public virtual User? User { get; set; }
}
