﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client_Home.Data;
using Client_Home.Models;
using iTextSharp.text;
using PagedList;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Client_Home.Controllers
{
    public class ProductsController : Controller
    {
        private readonly Data.ConveniencestoreContext _context;

        public ProductsController(Data.ConveniencestoreContext context)
        {
            _context = context;
        }

        // GET: Products
        public IActionResult Index(int? page, string searchString,string  CurrentFilter)
        {
            
            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = CurrentFilter;
            }
            var lsProducts = _context.Products.AsNoTracking()
                .OrderByDescending(x => x.ProductId);
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                lsProducts = (IOrderedQueryable<Product>)lsProducts.Where(x => x.Name.ToLower().Contains(searchString));

            }
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 9;
            PagedList.Core.IPagedList<Product> model = new PagedList.Core.PagedList<Product>(lsProducts, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;


            return View(model);

        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Include(p => p.ProductComments)
                   .ThenInclude(pc => pc.User)
                .Include(p => p.Ratings)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Description,SellPrice,TotalQuantity,CategoryId,ThumbnailUrl,VideoUrl,Discount,DiscountPrice,BestsellerFlag,HomeFlag,Active,SupplierId,DateAdded")] Product product)
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

        // GET: Products/Edit/5
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

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Description,SellPrice,TotalQuantity,CategoryId,ThumbnailUrl,VideoUrl,Discount,DiscountPrice,BestsellerFlag,HomeFlag,Active,SupplierId,DateAdded")] Product product)
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

        // GET: Products/Delete/5
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

        // POST: Products/Delete/5
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
        [HttpPost]
        public IActionResult AddComment(int productId, string commentText)
        {
            // Lấy sản phẩm từ cơ sở dữ liệu
            var product = _context.Products
                .Include(p => p.ProductComments)
                   .ThenInclude(pc => pc.User)
                .FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                return NotFound(); // Hoặc xử lý theo ý muốn của bạn
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // Tạo một đối tượng ProductComment mới
            var newComment = new ProductComment
            {
                ProductId = productId,
                CommentText = commentText,
                UserId = userId,
                CreatedDate = DateTime.Now
                // Nếu bạn muốn lưu thông tin người dùng, bạn có thể sử dụng User.Identity.Name hoặc thông tin từ đăng nhập
            };
            var existingComment = _context.ProductComments.Local.FirstOrDefault(pc => pc.CommentId == newComment.CommentId);
            if (existingComment != null)
            {
                _context.Entry(existingComment).State = EntityState.Detached;
            }
            // Thêm comment vào danh sách comment của sản phẩm
            product.ProductComments.Add(newComment);

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.SaveChanges();

            // Redirect hoặc trả về JSON hoặc thông báo thành công tùy thuộc vào yêu cầu của bạn
            return RedirectToAction("Details", new { id = productId });
        }
        [HttpPost]
        public IActionResult AddRating(int productId, int rating)
        {
            // Lấy sản phẩm từ cơ sở dữ liệu
            var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                return NotFound();
            }

            // Tạo đối tượng Rating mới
            var newRating = new Rating
            {
                ProductId = productId,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                StarRating = rating,
            };
            var existingRating = _context.Ratings.Local.FirstOrDefault(pc => pc.RatingId == newRating.RatingId);
            if (existingRating != null)
            {
                _context.Entry(existingRating).State = EntityState.Detached;
            }
            // Thêm rating vào danh sách rating của sản phẩm
            product.Ratings.Add(newRating);

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.SaveChanges();

            // Redirect hoặc trả về JSON hoặc thông báo thành công tùy thuộc vào yêu cầu của bạn
            return RedirectToAction("Details", new { id = productId });
        }

    }
}
