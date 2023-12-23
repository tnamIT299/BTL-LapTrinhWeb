using Client_Home.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Client_Home.Data;
using DocumentFormat.OpenXml.Spreadsheet;
using Client_Home.Areas.Admin.Models;

namespace Client_Home.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SearchController : Controller
    {
        private readonly Data.ConveniencestoreContext _context;
        public SearchController(Data.ConveniencestoreContext context)
        {
            _context = context;
        }


        [HttpPost]
        public IActionResult FindProducts(int page, string keyword)
        {
            var pageSize = 50;

            IQueryable<Product> ls = _context.Products
                            .AsNoTracking()
                            .OrderByDescending(x => x.ProductId);

            if (!string.IsNullOrEmpty(keyword))
            {
                ls = _context.Products
                    .AsNoTracking()
                    .Include(a => a.Category)
                    .Where(x => x.Name.Contains(keyword))
                    .OrderByDescending(x => x.Name);
            }

            if (page <= 0)
            {
                page = 1;
            }

            var paginatedItems = ls.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var models = new Pager<Product>
            {
                Items = paginatedItems,
                CurrentPage = page,
                PageIndex = page,
                PageSize = pageSize,
                TotalItems = ls.Count()
            };

            return PartialView("ListProductsSearchPartial", models);
        }


        [HttpPost]
        public IActionResult FindEmployees(int page, string keyword)
        {
            var pageSize = 50;

            IQueryable<Employee> ls = _context.Employees
                            .AsNoTracking()
                            .OrderByDescending(x => x.EmployeeId);

            if (!string.IsNullOrEmpty(keyword))
            {
                ls = _context.Employees
                .AsNoTracking()
                .Include(a => a.User)
                .Where(x => x.FirstName.Contains(keyword))
                .OrderByDescending(x => x.FirstName);
            }

            if (page <= 0)
            {
                page = 1;
            }

            var paginatedItems = ls.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var models = new Pager<Employee>
            {
                Items = paginatedItems,
                CurrentPage = page,
                PageIndex = page,
                PageSize = pageSize,
                TotalItems = ls.Count()
            };

            return PartialView("ListEmployeeSearchPartial", models);
        }
        [HttpPost]
        public IActionResult FindCustomers(int page, string keyword)
        {
            var pageSize = 50;

            IQueryable<Customer> ls = _context.Customers
                            .AsNoTracking()
                            .OrderByDescending(x => x.CustomerId);

            if (!string.IsNullOrEmpty(keyword))
            {
                ls = _context.Customers
                .AsNoTracking()
                .Where(x => x.LastName.Contains(keyword))
                .OrderByDescending(x => x.LastName);
            }

            if (page <= 0)
            {
                page = 1;
            }

            var paginatedItems = ls.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var models = new Pager<Customer>
            {
                Items = paginatedItems,
                PageIndex = page,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = ls.Count()
            };

            return PartialView("ListCustomerSearchPartial", models);
        }
        [HttpPost]
        public IActionResult FindInvoices(int page, string keyword)
        {
            var pageSize = 50;

            IQueryable<Invoice> ls = _context.Invoices
                            .AsNoTracking()
                            .OrderByDescending(x => x.InvoiceId);

            if (!string.IsNullOrEmpty(keyword))
            {
                ls = _context.Invoices
                .AsNoTracking()
                .Include(x => x.Customer)
                .Include(i => i.Employee)
                .Where(i => i.InvoiceId.ToString().Contains(keyword))
                .OrderByDescending(x => x.InvoiceId);
            }

            if (page <= 0)
            {
                page = 1;
            }

            var paginatedItems = ls.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var models = new Pager<Invoice>
            {
                Items = paginatedItems,
                PageIndex = page,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = ls.Count()
            };

            return PartialView("ListInvoicesSearchPartial", models);
        }
        [HttpPost]
        public IActionResult FindSuppliers(int page, string keyword)
        {
            var pageSize = 50;

            IQueryable<Supplier> ls = _context.Suppliers
                            .AsNoTracking()
                            .OrderByDescending(x => x.SupplierId);

            if (!string.IsNullOrEmpty(keyword))
            {
                ls = _context.Suppliers
                .AsNoTracking()
                .Include(i => i.Products)
                .Where(i => i.SupplierId.ToString().Contains(keyword))
                .OrderByDescending(x => x.SupplierId);
            }

            if (page <= 0)
            {
                page = 1;
            }

            var paginatedItems = ls.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var models = new Pager<Supplier>
            {
                Items = paginatedItems,
                PageIndex = page,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = ls.Count()
            };

            return PartialView("ListSupplierSearchPartial", models);
        }
        [HttpPost]
        public IActionResult FindOrders(int page, string keyword)
        {
            var pageSize = 50;

            IQueryable<Orders> ls = _context.Orders
                            .AsNoTracking()
                            .OrderByDescending(x => x.OrderId);

            if (!string.IsNullOrEmpty(keyword))
            {
                ls = _context.Orders
                .AsNoTracking()
                .Where(i => i.OrderId.ToString().Contains(keyword))
                .OrderByDescending(x => x.OrderId);
            }

            if (page <= 0)
            {
                page = 1;
            }

            var paginatedItems = ls.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var models = new Pager<Orders>
            {
                Items = paginatedItems,
                PageIndex = page,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = ls.Count()
            };

            return PartialView("ListOrdersSearchPartial", models);
        }
    }
}
