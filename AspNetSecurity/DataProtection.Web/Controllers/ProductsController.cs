using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataProtection.Web.Models;
using Microsoft.AspNetCore.DataProtection;

namespace DataProtection.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDataProtector _protector;

        public ProductsController(AppDbContext context, IDataProtector protector)
        {
            _context = context;
            _protector = protector.CreateProtector("Products");
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();

            //foreach (var product in products)
            //{
            //    product.EncrypedId = _protector.Protect(product.Id.ToString()); // id'yi şifreleyip EncrypedId'ye atıyoruz.
            //}

            foreach (var product in products)
            {
                product.EncrypedId = _protector.ToTimeLimitedDataProtector().Protect(product.Id.ToString(),TimeSpan.FromSeconds(5)); // id'yi şifreleyip EncrypedId'ye atıyoruz. 5 saniye sonra şifre çözülemez hale gelecek.
            }


            return View(products);
        }
        [HttpPost]
        public async Task<IActionResult> Index(string search)
        {
            var products = await _context.Products.FromSqlRaw("SELECT * FROM Products WHERE Name LIKE '%"+search+"%'").ToListAsync(); // sql injection saldırısına açık bir kod

            var products2= await _context.Products.FromSqlInterpolated($"SELECT * FROM Products WHERE Name LIKE '%{search}%'").ToListAsync(); // sql injection saldırısına karşı korunmuş kod

            var products3 = await _context.Products.FromSqlRaw("SELECT * FROM Products WHERE Name LIKE '%{0}%'", search).ToListAsync(); // sql injection saldırısına karşı korunmuş kod

            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //int decryptedId =int.Parse( _protector.Unprotect(id)); // id'yi çözüp decryptedId'ye atıyoruz.

            int decryptedId = int.Parse(_protector.ToTimeLimitedDataProtector().Unprotect(id)); // id'yi çözüp decryptedId'ye atıyoruz. 5 saniye sonra çözülemez hale gelecek.

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == decryptedId);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Color,ProductCategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Color,ProductCategoryId")] Product product)
        {
            if (id != product.Id)
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
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
