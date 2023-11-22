using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client_Home.Areas.DTO.Customers
{
    public class AddCustomer
    {
        //Validation for Customer 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập họ ")]
        [MaxLength(20)]
        [DataType(DataType.Text)]
        public string? FirstName { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập tên ")]
        [MaxLength(20)]
        [DataType(DataType.Text)]
        public string? LastName { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập email ")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [MaxLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập ngày tháng năm sinh")]
        public DateTime? Birthday { get; set; }

    }
}
