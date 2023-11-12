using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace Client_Home.Areas.Admin.DTO.Employees
{
    public class AddEmployees
    {
        [Required(ErrorMessage = "Vui lòng nhập họ ")]
        [MaxLength(20)]
        [DataType(DataType.Text)]
        [DisplayName("Họ")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [MaxLength(20)]
        [DataType(DataType.Text)]
        [DisplayName("Tên")]
        public string? LastName { get; set; }

        [DisplayName("Vị Trí")]
        [Required(ErrorMessage = "Vui lòng chọn vị trí")]
        public string? Position { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày vào làm")]
        [DisplayName("Ngày Vào Làm")]
        public DateTime? DateHired { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày tháng năm sinh")]
        [DisplayName("Ngày Sinh")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập căn cước công dân")]
        [DisplayName("Căn Cước Công Dân")]
        [MaxLength(12)]
        public string? CitizenId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        [DisplayName("Giới Tính")]
        public bool? Gender { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email ")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [DisplayName("Số Điện Thoại")]
        [MaxLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
    }
}
