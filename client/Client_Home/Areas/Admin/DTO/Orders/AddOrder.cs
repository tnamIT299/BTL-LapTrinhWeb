using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Client_Home.Areas.Admin.DTO.Orders
{
    public class AddOrder
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn nhà cung cấp ")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày tháng nhập hàng")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tổng tiền thanh toán")]
        public decimal? TotalAmount { get; set; }
    }
}
