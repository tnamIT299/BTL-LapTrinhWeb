using System.ComponentModel.DataAnnotations;

namespace Client_Home.Areas.DTO.Customers
{
    public class AddFromExcel
    {
        [Required(ErrorMessage = "Vui lòng chọn file Excel")]
        public string? fileExcel { get; set; }
    }
}
