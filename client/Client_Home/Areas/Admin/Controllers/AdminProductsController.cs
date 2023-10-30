﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client_Home.Data;
using Client_Home.Models;
using ClosedXML.Excel;

namespace Client_Home.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductsController : Controller
    {
        private readonly ConveniencestoreContext _context;

        public AdminProductsController(ConveniencestoreContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminProducts
        public async Task<IActionResult> Index()
        {
            var conveniencestoreContext = _context.Products.Include(p => p.Category).Include(p => p.Supplier);
            return View(await conveniencestoreContext.ToListAsync());
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
            return View();
        }

        // POST: Admin/AdminProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Description,SellPrice,TotalQuantity,CategoryId,ThumbnailUrl,VideoUrl,Discount,DiscountPrice,BestsellerFlag,HomeFlag,Active,SupplierId,DateAdded,Qrcode")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", product.SupplierId);
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", product.SupplierId);
            return View(product);
        }

        // POST: Admin/AdminProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Description,SellPrice,TotalQuantity,CategoryId,ThumbnailUrl,VideoUrl,Discount,DiscountPrice,BestsellerFlag,HomeFlag,Active,SupplierId,DateAdded,Qrcode")] Product product)
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


        public async Task<IActionResult> ExportToExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Products");
                // Set up font for worksheet
                worksheet.Style.Font.FontName = "Times New Roman";
                worksheet.Style.Font.FontSize = 13;
                // Set up style for header row
                var headerRow = worksheet.Row(1);
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Font.FontSize = 14;
                headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Merge cells for table title
                worksheet.Range("A1:J1").Merge();
                worksheet.Cell(1, 1).Value = "Danh sách sản phẩm";
                worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                // Fill color 
                worksheet.Range("A2:J2").Style.Fill.BackgroundColor = XLColor.Blue;
                worksheet.Range("A2:J2").Style.Font.FontColor = XLColor.White;
                worksheet.Range("A2:J2").Style.Font.Bold = true;
                // Set up header row content
                worksheet.Cell(2, 1).Value = "ID";
                worksheet.Cell(2, 2).Value = "Tên sản phẩm";
                worksheet.Cell(2, 3).Value = "Mô tả";
                worksheet.Cell(2, 4).Value = "Giá bán";
                worksheet.Cell(2, 5).Value = "Danh mục";
                worksheet.Cell(2, 6).Value = "Hình ảnh";
                worksheet.Cell(2, 7).Value = "Bán chạy";
                worksheet.Cell(2, 8).Value = "Trạng thái";
                worksheet.Cell(2, 9).Value = "Ngày thêm";
                worksheet.Cell(2, 10).Value = "Nhà cung cấp";

                var currentRow = 3;

                if (_context.Products != null)
                {
                    foreach (var product in await _context.Products.Include(p => p.Category).Include(p => p.Supplier).ToListAsync())
                    {
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

                        currentRow++;
                    }
                }
                for (int i = 1; i <= 10; i++)
                {
                    worksheet.Column(i).AdjustToContents();
                }       

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Products.xlsx");
                }
            }
        }

    }
}
