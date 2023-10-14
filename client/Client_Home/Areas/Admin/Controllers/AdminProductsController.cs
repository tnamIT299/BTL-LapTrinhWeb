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

namespace Client_Home.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductsController : Controller
    {
        private readonly ConveniencestoreContext _context;
        public INotyfService _notifyService { get; }
        public AdminProductsController(ConveniencestoreContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/AdminProducts
        public async Task<IActionResult> Index(int? page)
        {
            //var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            //var pageSize = 10;
            //var isProduct = _context.Products.Include(p => p.Category).Include(p=>p.Supplier).AsNoTracking().OrderByDescending(x => x.Name);
            //PagedList.Core.IPagedList<Product> models = new PagedList.Core.PagedList<Product>(isProduct, pageNumber, pageSize);
            //ViewBag.CurrentPage = pageNumber;
            //return View(models);
            return _context.Products != null ?
                         View(await _context.Products.Include(p =>p.Category).Include(p => p.Supplier).ToListAsync()) :
                         Problem("Entity set 'ConveniencestoreContext.Products'  is null.");
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId");
            return View();
        }

        // POST: Admin/AdminProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Description,SellPrice,TotalQuantity,CategoryId,ImageUrl,VideoUrl,Discount,DiscountPrice,BestsellerFlag,HomeFlag,Active,DateAdded,SupplierId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId", product.SupplierId);
            return View(product);
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId", product.SupplierId);
            return View(product);
        }

        // POST: Admin/AdminProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Description,SellPrice,TotalQuantity,CategoryId,ImageUrl,VideoUrl,Discount,DiscountPrice,BestsellerFlag,HomeFlag,Active,DateAdded,SupplierId")] Product product)
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId", product.SupplierId);
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
        // GET: Admin/AdminCustomers/ExportToExcel
        public async Task<IActionResult> ExportToExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Products");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "Tên sản phẩm";
                worksheet.Cell(currentRow, 3).Value = "Mô tả";
                worksheet.Cell(currentRow, 4).Value = "Giá bán";
                worksheet.Cell(currentRow, 5).Value = "Danh mục";
                worksheet.Cell(currentRow, 6).Value = "Hình ảnh";
                worksheet.Cell(currentRow, 7).Value = "Bán chạy";
                worksheet.Cell(currentRow, 8).Value = "Trạng thái";
                worksheet.Cell(currentRow, 9).Value = "Ngày thêm";
                worksheet.Cell(currentRow, 10).Value = "Nhà cung cấp";
                if (_context.Products != null)
                {
                    foreach (var product in await _context.Products.Include(p => p.Category).Include(p => p.Supplier).ToListAsync())
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = product.ProductId;
                        worksheet.Cell(currentRow, 2).Value = product.Name;
                        worksheet.Cell(currentRow, 3).Value = product.Description;
                        worksheet.Cell(currentRow, 4).Value = product.SellPrice;
                        worksheet.Cell(currentRow, 5).Value = product.Category.CategoryName;
                        worksheet.Cell(currentRow, 6).Value = product.ThumbnailUrl;
                        worksheet.Cell(currentRow, 7).Value = (XLCellValue)product.BestsellerFlag;
                        worksheet.Cell(currentRow, 8).Value = (XLCellValue)product.Active;
                        worksheet.Cell(currentRow, 9).Value = (XLCellValue)product.DateAdded;
                        worksheet.Cell(currentRow, 10).Value = product.Supplier.SupplierName;
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    //_notifyService.Success("Đã xuất file thành công");
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Products.xlsx");
                }
            }
        }
    }
}
