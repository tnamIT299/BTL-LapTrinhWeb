using Client_Home.Data;
using Client_Home.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Client_Home.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SearchController : Controller
    {
        private readonly Client_Home.Data.ConveniencestoreContext _context;
        public SearchController(Client_Home.Data.ConveniencestoreContext context)
        {
            _context = context;
        }


        [HttpPost]
        public IActionResult FindProduct(int? page, string keyword)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 20;

            IQueryable<Product> query;

            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                query = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Supplier)
                    .AsNoTracking()
                    .OrderByDescending(x => x.ProductId);
            }
            else
            {
                query = _context.Products
                    .AsNoTracking()
                    .Include(a => a.Category)
                    .Where(x => x.Name.Contains(keyword))
                    .OrderByDescending(x => x.Name);
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
                .Include(x=>x.Customer)
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

            List<Product> ls = query.ToList();
            PagedList.Core.IPagedList<Product> model = new PagedList.Core.PagedList<Product>(ls.AsQueryable(), pageNumber, pageSize);
            return PartialView("ListProductsSearchPartial", model);
        }


    }
}
