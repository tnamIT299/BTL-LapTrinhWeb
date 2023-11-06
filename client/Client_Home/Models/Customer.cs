using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client_Home.Models;

public partial class Customer
{
  
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerId { get; set; }


    [Required(ErrorMessage = "Vui lòng nhập họ ")]
    [MaxLength(20)]
    [DisplayName("Họ")]
    [DataType(DataType.Text)]
    public string? FirstName { get; set; }


    [Required(ErrorMessage = "Vui lòng nhập tên ")]
    [MaxLength(20)]
    [DataType(DataType.Text)]
    [DisplayName("Tên")]
    public string? LastName { get; set; }


    [Required(ErrorMessage = "Vui lòng nhập email ")]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }


    [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
    [MaxLength(10)]
    [DisplayName("Số Điện Thoại")]
    [DataType(DataType.PhoneNumber)]
    public string? Phone { get; set; }


    [Required(ErrorMessage = "Vui lòng nhập ngày tháng năm sinh")]
    [DisplayName("Ngày Sinh")]
    public DateTime? Birthday { get; set; }

    public int? RewardPoints { get; set; }

    public int? Rank { get; set; }

    public int? UserId { get; set; }

    [DisplayName("Giới Tính")]
    public int? Gender { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<ShippingLocation> ShippingLocations { get; set; } = new List<ShippingLocation>();

    public virtual User? User { get; set; }
}
