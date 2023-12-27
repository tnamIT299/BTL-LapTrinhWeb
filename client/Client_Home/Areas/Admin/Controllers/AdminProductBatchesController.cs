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
using Client_Home.Areas.Admin.DTO.ProductBatch;
using Client_Home.Areas.Admin.DTO.Employees;
using System.Data;
using Client_Home.Areas.Admin.Models;

namespace Client_Home.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductBatchesController : Controller
    {
        private readonly Data.ConveniencestoreContext _context;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly IAddProductBatchFromExcel _addFromExcel;
        private readonly ILogger<AdminProductBatchesController> _logger;
        public INotyfService _notifyService { get; }
        public AdminProductBatchesController(ILogger<AdminProductBatchesController> logger, Data.ConveniencestoreContext context, INotyfService notifyService, IWebHostEnvironment webHostEnvironment, IAddProductBatchFromExcel addFromExcel)
        {
            _logger = logger;
            _context = context;
            _notifyService = notifyService;
            _webHostEnvironment = webHostEnvironment;
            _addFromExcel = addFromExcel;
        }

        // GET: Admin/AdminProductBatches
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 15;
            var isPrBatch = _context.ProductBatches
            .AsNoTracking()
            .Include(p => p.Product).
            AsNoTracking().
            OrderByDescending(x => x.BatchId);
            PagedList.Core.IPagedList<ProductBatch> models = new PagedList.Core.PagedList<ProductBatch>(isPrBatch, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/AdminProductBatches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductBatches == null)
            {
                return NotFound();
            }

            var productBatch = await _context.ProductBatches
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.BatchId == id);
            if (productBatch == null)
            {
                return NotFound();
            }

            return View(productBatch);
        }

        // GET: Admin/AdminProductBatches/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            return View();
        }

        // POST: Admin/AdminProductBatches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BatchId,ProductId,ManufactureDate,ExpiryDate,Quantity,Barcode,ImportPrice")] ProductBatch productBatch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productBatch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", productBatch.ProductId);
            return View(productBatch);
        }

        public IActionResult AddFromExcel()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddFromExcel(IFormFile formFile)
        {
            string path = _addFromExcel.DoucumentUpload(formFile);
            DataTable dt = _addFromExcel.ProductBatchDataTable(path);
            _addFromExcel.ImportProductBatch(dt);
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminProductBatches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductBatches == null)
            {
                return NotFound();
            }

            var productBatch = await _context.ProductBatches.FindAsync(id);
            if (productBatch == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", productBatch.ProductId);
            return View(productBatch);
        }

        // POST: Admin/AdminProductBatches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BatchId,ProductId,ManufactureDate,ExpiryDate,Quantity,Barcode,ImportPrice")] ProductBatch productBatch)
        {
            if (id != productBatch.BatchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productBatch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductBatchExists(productBatch.BatchId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", productBatch.ProductId);
            return View(productBatch);
        }

        // GET: Admin/AdminProductBatches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductBatches == null)
            {
                return NotFound();
            }

            var productBatch = await _context.ProductBatches
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.BatchId == id);
            if (productBatch == null)
            {
                return NotFound();
            }

            return View(productBatch);
        }

        // POST: Admin/AdminProductBatches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductBatches == null)
            {
                return Problem("Entity set 'ConveniencestoreContext.ProductBatches'  is null.");
            }
            var productBatch = await _context.ProductBatches.FindAsync(id);
            if (productBatch != null)
            {
                _context.ProductBatches.Remove(productBatch);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool ProductBatchExists(int id)
        {
          return (_context.ProductBatches?.Any(e => e.BatchId == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> ExportToExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Product batches by products");
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
                worksheet.Cell(2, 1).Value = "Tên sản phẩm";
                worksheet.Cell(2, 2).Value = "Mã lô";
                worksheet.Cell(2, 3).Value = "Ngày sản xuất";
                worksheet.Cell(2, 4).Value = "Hạn sử dụng";
                worksheet.Cell(2, 5).Value = "Số lượng";
                worksheet.Cell(2, 6).Value = "Mã barcode";
                worksheet.Cell(2, 7).Value = "Giá nhập";
                worksheet.Cell(2, 8).Value = "Trạng thái";

                var currentRow = 3;

                if (_context.Products != null)
                {
                    foreach (var product in await _context.Products.Include(p => p.ProductBatches).ToListAsync())
                    {
                        int startRow = currentRow;
                        worksheet.Cell(currentRow, 1).Value = product.Name;
                        foreach( var productBatch in product.ProductBatches)
                        {
                            worksheet.Cell(currentRow, 2).Value = productBatch.BatchId;
                            worksheet.Cell(currentRow, 3).Value = productBatch.ManufactureDate;
                            worksheet.Cell(currentRow, 4).Value = productBatch.ExpiryDate;
                            worksheet.Cell(currentRow, 5).Value = productBatch.Quantity;
                            worksheet.Cell(currentRow, 6).Value = productBatch.Barcode;
                            worksheet.Cell(currentRow, 7).Value = productBatch.ImportPrice;
                            worksheet.Cell(currentRow, 8).Value = productBatch.ProductId;
                            currentRow++;
                        } 
                        if (startRow < currentRow - 1)
                        {
                            worksheet.Range(startRow, 1, currentRow - 1, 1).Merge();
                        }
                        
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
        [HttpPost("/Admin/AdminProductBatches/DeleteMultiple")]
        public async Task<IActionResult> UpdateStatusMultiple([FromBody] DeleteMulti itemIds)
        {
            try
            {
                if (_context.ProductBatches == null)
                {
                    return Problem("Entity set 'ConveniencestoreContext.ProductBatches' is null.");
                }

                var errors = new List<object>(); // List to store information about records that could not be updated

                foreach (var productBatchID in itemIds.itemIds)
                {
                    var ProductBatch = await _context.ProductBatches.FindAsync(productBatchID);

                    if (ProductBatch != null)
                    {
                        // Update the status of the product to inactive (or any other status you prefer)
                        //ProductBatch.Active = -1; // Assuming there is a property like IsActive in your Product entity

                        // Optionally, you can add some additional logic or validation before updating
                        // For example, check for foreign key references, business rules, etc.

                        _context.ProductBatches.Update(ProductBatch);
                    }
                    else
                    {
                        errors.Add(new { productBatchID, message = $"Lô sản phẩm có ID {productBatchID} không tồn tại." });
                    }
                }

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it appropriately
                    return Json(new { success = false, message = "Error updating product status.", errors });
                }

                if (errors.Any())
                {
                    // If there are errors, return the list of records that could not be updated along with a message
                    return Json(new { success = false, message = "Some product statuses were not updated.", errors });
                }

                // If there are no errors, return a success message
                return Json(new { success = true, message = "Cập nhật trạng thái sản phẩm thành công!" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Error updating product statuses." });
            }
        }
    }
}
