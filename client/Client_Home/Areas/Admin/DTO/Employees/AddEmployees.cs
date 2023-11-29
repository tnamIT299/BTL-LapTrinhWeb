using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client_Home.Areas.Admin.DTO.Employees
{
    public class AddEmployees
    {
        //Validation for Employee
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        public int? UserId { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập họ ")]
        [MaxLength(20)]
   
        [DataType(DataType.Text)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên ")]
        [MaxLength(20)]
        [DataType(DataType.Text)]
     
        public string? LastName { get; set; }

        public string? Position { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày vào làm")]
 
        public DateTime? DateHired { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày tháng năm sinh")]
       
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã CĂN CƯỚC CÔNG DÂN")]
        [MaxLength(12)]
     
        public string? CitizenId { get; set; }

   
        public int? Gender { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [MaxLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
    }
}
