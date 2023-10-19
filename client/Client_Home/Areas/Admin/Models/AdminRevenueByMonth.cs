using System.ComponentModel.DataAnnotations;

namespace Client_Home.Areas.Admin.Models
{
    public class AdminRevenueByMonth
    {
        [Key]
        public decimal Revenue { get; set; }
    }
}
