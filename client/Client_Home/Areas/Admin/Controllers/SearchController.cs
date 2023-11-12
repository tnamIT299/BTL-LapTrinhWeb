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
        public IActionResult FindProduct(string keyword)
        {
            List<Product> ls = new List<Product>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListProductsSearchPartial", null);
            }
            ls = _context.Products  
                .AsNoTracking()
                .Include(a=>a.Category)
                .Where(x=>x.Name.Contains(keyword))
                .OrderByDescending(x=>x.Name)
                .Take(10)
                .ToList();
            if(ls == null)
            {
                return PartialView("ListProductsSearchPartial", null);
            }
            else
            {
                return PartialView("ListProductsSearchPartial", ls);
            }
        }

    }
}
