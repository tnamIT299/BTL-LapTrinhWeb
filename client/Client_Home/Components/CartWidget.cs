using Client_Home.Infrastructure;
using Client_Home.Models;
using Microsoft.AspNetCore.Mvc;

namespace Client_Home.Components
{
    public class CartWidget : ViewComponent
    {
        public IViewComponentResult Invoke(){
            return View(HttpContext.Session.GetJson<Cart>("cart"));
        }
    }
}
