using System.ComponentModel.DataAnnotations;

namespace Client_Home.Areas.Admin.Models
{
    public class AdminOnlineOfflinePurchaseCount
    {
        [Key]
        public int OnlinePurchases { get; set; }
        public int OfflinePurchases { get; set; }
    }
}
