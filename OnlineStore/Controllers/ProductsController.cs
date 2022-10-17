using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.ViewModels;

namespace OnlineStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly StoreDbContext _context;

        public ProductsController(StoreDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string? title, int? category = null, SortingType sort = SortingType.TitleAsc, int page = 1)
        {
            var storeDbContext = _context.Products.Include(p => p.Category);
            ViewData["Categories"] = new SelectList(_context.Categories, "ID", "Title");
            int maxCount = 20;


            IQueryable<Product> query = storeDbContext;

            query = sort switch {
                SortingType.TitleAsc => query.OrderBy(p=>p.Title),
                SortingType.TitleDesc => query.OrderByDescending(p => p.Title),
                SortingType.PriceAsc => query.OrderBy(p => p.Price),
                SortingType.PriceDesc => query.OrderByDescending(p => p.Price),
                SortingType.CategoryAsc => query.OrderBy(p => p.Category),
                SortingType.CategoryDesc => query.OrderByDescending(p => p.Category),
                _ => query.OrderBy(p => p.Title),
            };

            FilteringViewModel filtering = new FilteringViewModel(title, category, ref query);

            var totalCountForFilter = await query.CountAsync();

            query = query.Skip((page - 1)*maxCount).Take(maxCount);
            
            var res = await query.ToListAsync();

            var vm = new ProductsListViewModel() {
                Result = res,
                PaginationViewModel = new PaginationViewModel(totalCountForFilter, page, maxCount),
                SortingViewModel = new SortingViewModel(sort),
                FilteringViewModel = filtering
            };

            return View(vm);
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
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            Random random = new Random();
            for ( int i = 0; i < 1000; i++ ) {
                _context.Add(new Product() {
                    Description = $"test desk for product {i}",
                    ID_Category = random.Next(1, 4),
                    Price = random.Next(100, 1000) + random.NextDouble(),
                    Title = $"Product {i}",
                });
            }
            _context.SaveChanges();
            //ViewData["ID_Category"] = new SelectList(_context.Categories, "ID", "Title");
            return View("Index");
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,Price,ID_Category")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ID_Category"] = new SelectList(_context.Categories, "ID", "Title", product.ID_Category);
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
            ViewData["ID_Category"] = new SelectList(_context.Categories, "ID", "Title", product.ID_Category);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,Price,ID_Category")] Product product)
        {
            if (id != product.ID)
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
                    if (!ProductExists(product.ID))
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
            ViewData["ID_Category"] = new SelectList(_context.Categories, "ID", "Title", product.ID_Category);
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
                .FirstOrDefaultAsync(m => m.ID == id);
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
                return Problem("Entity set 'StoreDbContext.Products'  is null.");
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
          return _context.Products.Any(e => e.ID == id);
        }
    }
}
