using Concurreny.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concurreny.Web.Controllers;

public class ProductsController : Controller
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Update(int id)
    {
        var product = await _context.Products.FindAsync(id);
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Update(Product product)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var clientValues = (Product)entry.Entity;
                var databaseEntry = entry.GetDatabaseValues();
                var databaseProduct = (Product)databaseEntry.ToObject();


                ModelState.AddModelError("", ex.Message);
            }

        }
        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var products = await _context.Products.ToListAsync();
        return View(products);
    }
}
