using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client_Home.Data;
using Client_Home.Models;

namespace Client_Home.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductBatchesController : Controller
    {
        private readonly Client_Home.Data.ConveniencestoreContext _context;

        public AdminProductBatchesController(Client_Home.Data.ConveniencestoreContext context)
        {
            _context = context;
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productBatch.ProductId);
            return View(productBatch);
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productBatch.ProductId);
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productBatch.ProductId);
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
    }
}
