using Client_Home.Data;
using Client_Home.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            List<Product> ls = query.ToList();
            PagedList.Core.IPagedList<Product> model = new PagedList.Core.PagedList<Product>(ls.AsQueryable(), pageNumber, pageSize);
            return PartialView("ListProductsSearchPartial", model);
        }


    }
}
