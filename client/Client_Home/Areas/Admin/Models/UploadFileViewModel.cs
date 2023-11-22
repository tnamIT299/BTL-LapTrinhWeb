using System.ComponentModel.DataAnnotations;

namespace Client_Home.Areas.Admin.Models
{
    public class UploadFileViewModel
    {
        [Required(ErrorMessage = "Vui lòng chọn một tệp Excel.")]
        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }
    }
}
