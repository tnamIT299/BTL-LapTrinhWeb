using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client_Home.Models;

public partial class Customer
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập họ ")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên ")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập email ")]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập SĐT")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập NGÀY THÁNG NĂM SINH ")]
    public DateTime? Birthday { get; set; }

    public int? RewardPoints { get; set; }

    public int? Rank { get; set; }

    public int? UserId { get; set; }

    public bool? Gender { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<ShippingLocation> ShippingLocations { get; set; } = new List<ShippingLocation>();

    public virtual User? User { get; set; }
}
