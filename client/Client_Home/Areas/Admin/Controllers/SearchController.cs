using Microsoft.AspNetCore.Mvc;

namespace Client_Home.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
