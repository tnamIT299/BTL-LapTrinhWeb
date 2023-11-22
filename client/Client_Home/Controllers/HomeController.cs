﻿using Client_Home.Data;
using Client_Home.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Client_Home.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Client_Home.Data.ConveniencestoreContext _context;
        public HomeController(ILogger<HomeController> logger, Client_Home.Data.ConveniencestoreContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var conveniencestoreContext = _context.Products.Include(p => p.Category).Include(p => p.Supplier);
            return View(await conveniencestoreContext.ToListAsync());
        }
        public IActionResult Shop()
        {
            return View();
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}