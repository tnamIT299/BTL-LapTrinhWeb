using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client_Home.Areas.Admin.DTO.ProductBatch
{
    public class AddPrBatch
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BatchId { get; set; }

        public int ProductId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày sản xuất lô hàng")]
        public DateTime? ManufactureDate { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập hạn sử dụng lô hàng")]
        public DateTime? ExpiryDate { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng sản phẩm thuộc lô hàng")]
        public int? Quantity { get; set; }

        public string? Barcode { get; set; }

        [Required(ErrorMessage = "Vui lòng điền giá nhập")]
        public decimal? ImportPrice { get; set; }
    }
}
