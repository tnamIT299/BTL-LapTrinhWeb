using System.ComponentModel.DataAnnotations;
namespace Client_Home.Areas.Admin.DTO.Employees

{
    public class AddFromExcel
    {
        [Required(ErrorMessage = "Vui lòng chọn file Excel")]
        public string? fileExcel { get; set; }
    }
}
