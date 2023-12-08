using Client_Home.Data;
using Client_Home.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Client_Home.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Data.ConveniencestoreContext _context;
        public HomeController(ILogger<HomeController> logger, Data.ConveniencestoreContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var conveniencestoreContext = _context.Products.Include(p => p.Category).Include(p => p.Supplier);
            return View(await conveniencestoreContext.ToListAsync());
        }
        public IActionResult Shop(int? maloai)
        {
            IQueryable<Product> productsQuery = _context.Products;

            if (maloai.HasValue)
            {
                // Filter products based on the specified category
                productsQuery = productsQuery.Where(p => p.CategoryId == maloai.Value);
            }
            else
            {
                // Display a default category (adjust the CategoryId accordingly)
                // For example, you might want to display products from the first category
                productsQuery = productsQuery.Where(p => p.CategoryId == 1);
            }
            List<Product> lstProduct = productsQuery.OrderBy(p => p.Name).ToList();
            // List<Product> lstProduct = _context.Products.Where(p => p.CategoryId == maloai).OrderBy(p => p.Name).ToList();
            return View(lstProduct);
        }
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult LoaiSP()
        {
            List<Category> lstProduct = _context.Categories.ToList();
            return View(lstProduct);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}