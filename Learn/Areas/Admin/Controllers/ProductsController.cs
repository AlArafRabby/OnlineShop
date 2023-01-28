using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Learn.Data;
using Learn.Models;
using Learn.DTO;

namespace Learn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly OnlineShopJoyContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _he;
        public ProductsController(OnlineShopJoyContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment he)
        {
            _context = context;
            _he = he;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            return View(_context.Products.Include(c => c.ProductTypes).Include(f => f.SpecialTag).ToList());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductTypes)
                .Include(p => p.SpecialTag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "ProductTypes");
            ViewData["SpecialTagId"] = new SelectList(_context.SpecialTags, "Id", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDTO product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var searchProduct = _context.Products.FirstOrDefault(c => c.Name == product.Name);
                if (searchProduct != null)
                {
                    ViewBag.message = "This product is already exist";
                    ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "ProductTypes", product.ProductTypeId);
                    ViewData["SpecialTagId"] = new SelectList(_context.SpecialTags, "Id", "Name", product.SpecialTagId);
                    return View(product);
                }
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    product.Image = "Images/noimage.PNG";
                }
                var detalis = new Products
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Image = product.Image,
                    ProductColor = product.ProductColor,
                    IsAvailable = product.IsAvailable,
                    ProductTypeId = product.ProductTypeId,
                    SpecialTagId = product.SpecialTagId,
                };

                _context.Add(detalis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // GET: Admin/Products/Edit/5
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
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "ProductTypes", product.ProductTypeId);
            ViewData["SpecialTagId"] = new SelectList(_context.SpecialTags, "Id", "Name", product.SpecialTagId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Image,ProductColor,IsAvailable,ProductTypeId,SpecialTagId")] ProductDTO product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var detalis = new Products
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Image = product.Image,
                        ProductColor = product.ProductColor,
                        IsAvailable = product.IsAvailable,
                        ProductTypeId = product.ProductTypeId,
                        SpecialTagId = product.SpecialTagId,
                    };



                    _context.Update(detalis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "ProductTypes", product.ProductTypeId);
            ViewData["SpecialTagId"] = new SelectList(_context.SpecialTags, "Id", "Name", product.SpecialTagId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductTypes)
                .Include(p => p.SpecialTag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'OnlineShopJoyContext.Products'  is null.");
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
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
