using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Client_Home.Areas.Admin.DTO.Suppliers
{
    public class AddSupplier
    {
        [Required(ErrorMessage = "Vui lòng nhập tên nhà cung cấp ")]
        [DisplayName("Tên Nhà Cung Cấp")]
        public string? SupplierName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên liên hệ ")]
        [DisplayName("Tên Liên Hệ")]
        public string? ContactName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        [DisplayName("Địa chỉ")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên thành phố")]
        [DisplayName("Thành Phố")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã PostCode")]
        [DisplayName("Mã PostCode")]
        public string? PostalCode { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên quốc gia ")]
        [DisplayName("Quốc Gia")]
        public string? Country { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Số điện thoại")]
        [DisplayName("Số Điện Thoại")]
        [MaxLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
    }
}
