using System.ComponentModel.DataAnnotations;

namespace Client_Home.Areas.Admin.Models
{
    public class AdminRichestCustomerView
    {
        [Key]
        public int CustomerID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public decimal TotalPaid { get; set; }
    }
}
