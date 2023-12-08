using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client_Home.Data;
using Client_Home.Models;
using ClosedXML.Excel;
using AspNetCoreHero.ToastNotification.Abstractions;
using PagedList;
using Humanizer;
using System.Data;
using Client_Home.Areas.Admin.DTO.Product;

namespace Client_Home.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductsController : Controller
    {
        private readonly Client_Home.Data.ConveniencestoreContext _context;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly IAddProductFromExcel _addFromExcel;
        private readonly ILogger<AdminProductsController> _logger;
        public INotyfService _notifyService { get; }
        public AdminProductsController(ILogger<AdminProductsController> logger, ConveniencestoreContext context, INotyfService notifyService, IWebHostEnvironment webHostEnvironment, IAddProductFromExcel addFromExcel)
        {
            _logger = logger;
            _context = context;
            _notifyService = notifyService;
            _webHostEnvironment = webHostEnvironment;
            _addFromExcel = addFromExcel;
        }

        // GET: Admin/AdminProducts
        public IActionResult Index(int page = 1, int CatID = 0, int SupID = 0,decimal? to=null,decimal? from=null)

        {
            var pageNumber = page;
            var pageSize = 10;
            List<Product> isProducts = new List<Product>();
            if (SupID != 0)
            {
                isProducts = _context.Products
                    .AsNoTracking().Where(x => x.SupplierId == SupID)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .OrderByDescending(x => x.ProductId).ToList();
            }
            else if (CatID != 0)
            {
                isProducts = _context.Products
                    .AsNoTracking().Where(x => x.CategoryId == CatID)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .OrderByDescending(x => x.ProductId).ToList();
            }
            else
            {
                isProducts = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier).
                AsNoTracking().
                OrderByDescending(x => x.ProductId).ToList();
                if (to != null && from != null)
                {
                    isProducts = _context.Products
                    .AsNoTracking()
                       .Where(x => x.SellPrice >= to && x.SellPrice <= from)
                        .Include(p => p.Category)
                    .Include(p => p.Supplier)
                    .OrderByDescending(x => x.ProductId).ToList();
                    ;
                }
            }


            PagedList.Core.IPagedList<Product> model = new PagedList.Core.PagedList<Product>(isProducts.AsQueryable(), pageNumber, pageSize);
            ViewBag.CurrentCateID = CatID;
            ViewBag.CurrentPage = pageNumber;
            ViewData["Danhmuc"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", CatID);
            ViewData["Nhacungcap"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName",SupID);


            return View(model);
            // return _context.Products != null ?
            //View(await _context.Products.Include(p =>p.Category).Include(p => p.Supplier).ToListAsync()) :
            //Problem("Entity set 'ConveniencestoreContext.Products'  is null.");
        }
        public IActionResult Filtter(int CatID = 0)
        {
            var url = $"/Admin/AdminProducts?CatID={CatID}";
            if (CatID == 0)
            {
                url = $"/Admin/AdminProducts";
            }
            return Json(new { status = "success", redirectUrl = url });
        }
        public IActionResult FiltterSupplier(int SupID = 0)
        {
            var url = $"/Admin/AdminProducts?SupID={SupID}";
            if (SupID == 0)
            {
                url = $"/Admin/AdminProducts";
            }
            return Json(new { status = "success", redirectUrl = url });
        }

        // GET: Admin/AdminProducts/Details/5   
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Include(p => p.ProductBatches)
                .Include(p => p.ProductSubImages)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/AdminProducts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName");
            ViewData["Danhmuc"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Admin/AdminProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Description,SellPrice,TotalQuantity,CategoryId,ThumbnailUrl,VideoUrl,Discount,DiscountPrice,BestsellerFlag,HomeFlag,Active,SupplierId,DateAdded,Qrcode,Unit")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", product.SupplierId);
            ViewData["Danh muc"] = new SelectList(_context.Categories, "CatId", "CatName");
            return View(product);
        }

        public IActionResult AddFromExcel()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddFromExcel(IFormFile formFile)
        {
            string path = _addFromExcel.DoucumentUpload(formFile);
            DataTable dt = _addFromExcel.ProductDataTable(path);
            _addFromExcel.ImportProduct(dt);
            return RedirectToAction(nameof(Index));

        }
        // GET: Admin/AdminProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", product.SupplierId);
            ViewData["Danh muc"] = new SelectList(_context.Categories, "CatId", "CatName");
            return View(product);
        }

        // POST: Admin/AdminProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Description,SellPrice,TotalQuantity,CategoryId,ThumbnailUrl,VideoUrl,Discount,DiscountPrice,BestsellerFlag,HomeFlag,Active,SupplierId,DateAdded,Qrcode,Unit")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", product.SupplierId);
            ViewData["Danh muc"] = new SelectList(_context.Categories, "CatId", "CatName");
            return View(product);
        }

        // GET: Admin/AdminProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/AdminProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ConveniencestoreContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
