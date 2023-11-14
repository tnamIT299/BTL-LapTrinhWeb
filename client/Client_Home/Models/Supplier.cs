using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client_Home.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên nhà cung cấp ")]
    [DisplayName("Tên Nhà Cung Cấp")]
    [DataType(DataType.Text)]
    public string SupplierName { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập tên liên hệ NCC ")]
    [DisplayName("Tên Liên Hệ")]
    [DataType(DataType.Text)]
    public string? ContactName { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập địa chỉ NCC ")]
    [DisplayName("Địa Chỉ")]
    [DataType(DataType.Text)]
    public string? Address { get; set; }

   
    [DisplayName("Thành Phố")]
    public string? City { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập mã PostCode ")]
    [DisplayName("Mã PostCode")]
    [DataType(DataType.Text)]
    public string? PostalCode { get; set; }

    [DisplayName("Quốc Gia")]
    public string? Country { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
    [DisplayName("Điện Thoại")]
    [DataType(DataType.PhoneNumber)]
    public string? Phone { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
