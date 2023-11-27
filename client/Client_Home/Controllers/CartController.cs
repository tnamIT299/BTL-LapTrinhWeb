﻿using Microsoft.AspNetCore.Mvc;
using Client_Home.Models;
using Client_Home.Data;
using Client_Home.Infrastructure;

namespace Client_Home.Controllers
{
    public class CartController : Controller
    {
        public Cart? Cart { get; set; }
        private readonly Client_Home.Data.ConveniencestoreContext _context;

        public CartController(Client_Home.Data.ConveniencestoreContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View("Cart",HttpContext.Session.GetJson<Cart>("cart"));
        }
        public IActionResult AddToCart(int productId)
        {
            Product? product = _context.Products
                .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }


            return View("Cart",Cart);
        }
        public IActionResult UpdateCart(int productId)
        {
            Product? product = _context.Products
                .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, -1);
                HttpContext.Session.SetJson("cart", Cart);
            }


            return View("Cart", Cart);
        }
        public IActionResult RemoveFromCart(int productId)
        {
            Product? product = _context.Products
                .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart");
                Cart.RemoveLine(product);
                HttpContext.Session.SetJson("cart", Cart);
            }


            return View("Cart", Cart);
        }
    }
}
