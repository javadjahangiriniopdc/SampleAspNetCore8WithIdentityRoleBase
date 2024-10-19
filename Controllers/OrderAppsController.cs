using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SampleAspNetCore8WithIdentityRoleBase.Data;
using SampleAspNetCore8WithIdentityRoleBase.Models;

namespace SampleAspNetCore8WithIdentityRoleBase.Controllers
{
    public class OrderAppsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderAppsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderApps
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OrderApps.Include(o => o.Customer).Include(o => o.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OrderApps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderApp = await _context.OrderApps
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderApp == null)
            {
                return NotFound();
            }

            return View(orderApp);
        }

        // GET: OrderApps/Create
        public IActionResult Create()
        {
            // تنظیم تاریخ و زمان جاری
            var currentDateTime = DateTime.Now;

            // پاس دادن تاریخ و زمان به View
            ViewBag.CreatAt = currentDateTime;
            ViewBag.UpdateAt = currentDateTime;

            ViewData["CustomerID"] = new SelectList(_context.Customers.Select(c => new
            {
                c.Id,
                FullName = c.Name + " " + c.LastName
            }), "Id", "FullName");

            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: OrderApps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerID,ProductID,Quantity,Price,PriceAll,CreatAt,UpdateAt")] OrderApp orderApp)
        {
            if (ModelState.IsValid)
            {
                // تنظیم تاریخ و زمان جاری
                var currentDateTime = DateTime.Now;
                orderApp.CreatAt= currentDateTime;
                orderApp.UpdateAt = currentDateTime;

                _context.Add(orderApp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "Id", "Id", orderApp.CustomerID);
            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Name", orderApp.ProductID);
            return View(orderApp);
        }

        // GET: OrderApps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderApp = await _context.OrderApps.FindAsync(id);
            if (orderApp == null)
            {
                return NotFound();
            }
            // تنظیم تاریخ و زمان جاری
            var currentDateTime = DateTime.Now;

          

            ViewData["CustomerID"] = new SelectList(_context.Customers.Select(c => new
            {
                c.Id,
                FullName = c.Name + " " + c.LastName
            }), "Id", "FullName");

            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Name", orderApp.ProductID);
            return View(orderApp);
        }

        // POST: OrderApps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerID,ProductID,Quantity,Price,PriceAll,CreatAt,UpdateAt")] OrderApp orderApp)
        {
            if (id != orderApp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   

                    // بروزرسانی تاریخ به روزرسانی
                    orderApp.UpdateAt = DateTime.Now;
                    _context.Update(orderApp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderAppExists(orderApp.Id))
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

            ViewData["CustomerID"] = new SelectList(_context.Customers.Select(c => new
            {
                c.Id,
                FullName = c.Name + " " + c.LastName
            }), "Id", "FullName");
            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Name", orderApp.ProductID);
            return View(orderApp);
        }

        // GET: OrderApps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderApp = await _context.OrderApps
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderApp == null)
            {
                return NotFound();
            }

            return View(orderApp);
        }

        // POST: OrderApps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderApp = await _context.OrderApps.FindAsync(id);
            if (orderApp != null)
            {
                _context.OrderApps.Remove(orderApp);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderAppExists(int id)
        {
            return _context.OrderApps.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductPrice(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            return Json(product.Price);
        }

    }
}
