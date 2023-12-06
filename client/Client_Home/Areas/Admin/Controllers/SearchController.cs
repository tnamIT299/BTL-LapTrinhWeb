using Client_Home.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Client_Home.Data;
namespace Client_Home.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SearchController : Controller
    {
        private readonly ConveniencestoreContext _context;
        public SearchController(ConveniencestoreContext context)
        {
            _context = context;
        }


        [HttpPost]
        public IActionResult FindProduct(int? page, string keyword)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 15;
            IQueryable<Product> ls = _context.Products
                            .AsNoTracking()
                            .OrderByDescending(x => x.ProductId);
            PagedList.Core.IPagedList<Product> models = new PagedList.Core.PagedList<Product>(ls, pageNumber, pageSize);
            if (string.IsNullOrEmpty(keyword) || keyword == null)
            {
                return PartialView("ListProductsSearchPartial", models);
            }
            ls = _context.Products
                    .AsNoTracking()
                    .Include(a => a.Category)
                    .Where(x => x.Name.Contains(keyword))
                    .OrderByDescending(x => x.Name);
            models = new PagedList.Core.PagedList<Product>(ls, pageNumber, pageSize);
            if (ls == null)
            {
                return PartialView("ListProductsSearchPartial", null);
            }
            else
            {
                return PartialView("ListProductsSearchPartial", models);
            }
        }
        [HttpPost]
        public IActionResult FindEmployee(string keyword)
        {
            List<Employee> ls = new List<Employee>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListEmployeeSearchPartial", null);
            }
            ls = _context.Employees
                .AsNoTracking()
                .Include(a => a.User)
                .Where(x => x.FirstName.Contains(keyword))
                .OrderByDescending(x => x.FirstName)
                .Take(10)
                .ToList();
            if (ls == null)
            {
                return PartialView("ListEmployeeSearchPartial", null);
            }
            else
            {
                return PartialView("ListEmployeeSearchPartial", ls);
            }
        }
        [HttpPost]
        public IActionResult FindCustomer(string keyword)
        {
            List<Customer> ls = new List<Customer>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListCustomerSearchPartial", null);
            }
            ls = _context.Customers
                .AsNoTracking()
                .Where(x => x.LastName.Contains(keyword))
                .OrderByDescending(x => x.LastName)
                .Take(10)
                .ToList();
            if (ls == null)
            {
                return PartialView("ListCustomerSearchPartial", null);
            }
            else
            {
                return PartialView("ListCustomerSearchPartial", ls);
            }
        }
        [HttpPost]
        public IActionResult FindInvoices(string keyword)
        {
            List<Invoice> ls = new List<Invoice>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListCustomerSearchPartial", null);
            }
            ls = _context.Invoices
                .AsNoTracking()
                .Include(x => x.Customer)
                .Include(i => i.Employee)
                .Where(i => i.InvoiceId.ToString().Contains(keyword))
                .OrderByDescending(x => x.InvoiceId)
                .Take(10)
                .ToList();
            if (ls == null)
            {
                return PartialView("ListInvoicesSearchPartial", null);
            }
            else
            {
                return PartialView("ListInvoicesSearchPartial", ls);
            }
        }


    }
}
