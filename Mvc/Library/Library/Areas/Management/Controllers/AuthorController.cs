using Library.Models;
using Library.RepositoryPattern.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Library.Areas.Management.Controllers
{
    [Authorize(Policy ="AdminPolicy")]
    [Area("Management")]
    public class AuthorController : Controller
    {
        //MyDbContext _db;
        IAuthorRepository _repoAuthor;
        public AuthorController(//MyDbContext db,
            IAuthorRepository repoAuthor)
        {
            //_db = db;
            _repoAuthor = repoAuthor;
        }

        
        public IActionResult AuthorList()
        {
            //List<Author> authors = _db.Authors.ToList();
            
            List<Author> authors = _repoAuthor.GetActives();
            return View(authors);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Author author)
        {
            //_db.Authors.Add(author);
            //_db.SaveChanges();
            if (!ModelState.IsValid)
            {
                return View(author);
            }
            _repoAuthor.Add(author);
            return RedirectToAction("AuthorList","Author",new {area= "Management"});
        }
        public IActionResult Edit(int id)
        {
            Author author = _repoAuthor.GetById(id);
            return View(author);
        }
        [HttpPost]
        public IActionResult Edit(Author author)
        {
            _repoAuthor.Update(author);
            return RedirectToAction("AuthorList", "Author", new { area = "Management" });
        }
        public IActionResult Delete(int id)
        {
            /*
            Author author = _db.Authors.Find(id);
            author.Status = Enums.DataStatus.Deleted;
            author.ModifiedDate = DateTime.Now;
            _db.Authors.Update(author);
            _db.SaveChanges();*/
            _repoAuthor.SpecialDelete(id);
            return RedirectToAction("AuthorList", "Author", new { area = "Management" });
        }

    }
}
