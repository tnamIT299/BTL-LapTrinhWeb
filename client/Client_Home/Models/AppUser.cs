using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client_Home.Models
{
    public class AppUser : IdentityUser
    {
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Column(TypeName = "nvarchar(250)")]
        [Display(Name = "Họ")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Tên")]
        [Column(TypeName = "nvarchar(250)")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Địa chỉ")]
        [Column(TypeName = "nvarchar(250)")]
        public string? Address { get; set; }

       
    }
}