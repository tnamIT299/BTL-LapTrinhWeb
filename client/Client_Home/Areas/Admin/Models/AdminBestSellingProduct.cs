using System.ComponentModel.DataAnnotations;

namespace Client_Home.Areas.Admin.Models
{
    public class AdminBestSellingProduct
    {
        [Key]
        public int ProductID { get; set; }
        public string? Name { get; set; }
        public decimal SellPrice { get; set; }
        public int TotalQuantitySold { get; set; }

    }
}
