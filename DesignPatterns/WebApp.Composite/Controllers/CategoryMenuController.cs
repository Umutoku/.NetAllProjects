using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApp.Composite.Composites;
using WebApp.Composite.Models;

namespace WebApp.Composite.Controllers
{
    [Authorize]
    public class CategoryMenuController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public CategoryMenuController(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //category => bookcomposite
            //book => bookcomponent

            var userID = User.FindFirst(ClaimTypes.NameIdentifier).Value; // Kullanıcı ID'si alındı, Startup.cs dosyasında tanımlanan AddDefaultIdentity metodu içerisinde tanımlanan User.RequireUniqueEmail = true; özelliğinden dolayıdır.

            var categories = await _context.Categories
                .Include(c => c.Books)
                .Where(x=>x.UserId == userID)
                .OrderBy(x=>x.Id).ToListAsync();

            var menu = GetMenus(categories, new Category() { Id = 0, Name = "TopCategory" }, new BookComposite(0,"TopBook") );

            ViewBag.menu = menu;

            ViewBag.selectList = menu.Components.SelectMany(x => ((BookComposite)x).GetSelectListItems(""));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(int categoryId, string bookName)
        {
            await _context.Books.AddAsync(new Book() { Name = bookName, CategoryId = categoryId });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public BookComposite GetMenus(List<Category> categories, Category category, BookComposite topBookComposite, BookComposite last=null)
        {
            // aşağıda kategorilerin alt kategorileri ve kitapları getirilir.
            categories.Where(x => x.ReferenceId == category.Id).ToList().ForEach(x =>
            {
                var bookComposite = new BookComposite(x.Id, x.Name);
                x.Books.ToList().ForEach(b =>
                {
                    bookComposite.AddComponent(new BookComponent(b.Id, b.Name)); // kitaplar eklenir 
                });

                if (last != null)
                {
                    last.AddComponent(bookComposite); // alt kategoriler eklenir 
                }
                else
                {
                    topBookComposite.AddComponent(bookComposite); // ana kategoriler eklenir
                }
                // recursive sorgu ile alt kategorileri getir
                GetMenus(categories, x, topBookComposite, bookComposite);
                
            });

            return topBookComposite; // ana kategoriler döndürülür
        }
    }
}
