using Client_Home.Areas.Admin.Models;
using Client_Home.Data;
using Client_Home.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Client_Home.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (var context = new Data.ConveniencestoreContext())
            {
                var invoices = await context.Invoices.FromSqlRaw("EXEC GetLatestInvoices").ToListAsync();
                var TopCustomers = await context.AdminRichestCustomerView.FromSqlRaw("EXEC GetTopCustomers").ToListAsync();
                var TopProduct = await context.AdminBestSellingProduct.FromSqlRaw("EXEC GetTop5Products").ToListAsync();
                var revenueByMonth = await context.AdminRevenueByMonth.FromSqlRaw("EXEC GetRevenueByMonth").ToListAsync();
                var profitByMonth = await context.AdminRevenueByMonth.FromSqlRaw("EXEC CalculateMonthlyProfit").ToListAsync();
                var onlineOfflineCountByMonth = await context.AdminOnlineOfflinePurchaseCount.FromSqlRaw("EXEC CalculateOnlineOfflinePurchases").ToListAsync();
                var numberInvoiceThisMonth = await context.AdminSingleIntForProcedure.FromSqlRaw("EXEC GetInvoiceCountForCurrentMonth").ToListAsync();
                var numberInvoiceLastMonth = await context.AdminSingleIntForProcedure.FromSqlRaw("EXEC GetTotalOrdersLastMonth;").ToListAsync();

                ViewBag.numberInvoiceLastMonth = numberInvoiceLastMonth;
                ViewBag.numberInvoiceThisMonth = numberInvoiceThisMonth;
                ViewBag.onlineOfflineCountByMonth = onlineOfflineCountByMonth;
                ViewBag.profitByMonth = profitByMonth;
                ViewBag.revenueByMonth = revenueByMonth;
                ViewBag.TopCustomers = TopCustomers;
                ViewBag.invoices = invoices;
                ViewBag.TopProduct = TopProduct;
                return View();
            }
           
        }
        
        //public async Task<List<Invoice>> GetLatestInvoicesAsync()
        //{
        //    using (var context = new ConveniencestoreContext())
        //    {
                
        //    }
        //}

    }
}
