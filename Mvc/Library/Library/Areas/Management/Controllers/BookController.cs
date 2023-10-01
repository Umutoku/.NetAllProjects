using Library.Context;
using Library.Dto;
using Library.Models;
using Library.RepositoryPattern.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Library.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize(Policy ="AdminPolicy")]
    public class BookController : Controller
    {
        MyDbContext _db;
        IBookRepository _repoBook;
        IAuthorRepository _repoAuthor;
        IBookTypeRepository _repoBookType;
        ILogger<BookController> _logger;
        
        public BookController(MyDbContext db,
            IBookRepository repoBook,
            IAuthorRepository repoAuthor,
            IBookTypeRepository repoBookType,
            ILogger<BookController> logger)
        {
            _db = db;
            _repoBook = repoBook;
            _repoAuthor = repoAuthor;
            _repoBookType = repoBookType;
            _logger = logger;
        }
        public IActionResult BookList()
        {
            List<Book> books = _repoBook.GetBooks();
            return View(books);
        }
        public IActionResult Create()
        {
           List<AuthorDto> authors = _repoAuthor.SelectAuthorDto();

            List<BookTypeDto> bookTypes = _repoBookType.SelectBookTypeDto();
            return View((new Book(),authors,bookTypes));
        }
        [HttpPost]
        public IActionResult Create([Bind(Prefix ="Item1")] Book book)
        {
            if(!ModelState.IsValid)
            {
                List<AuthorDto> authors = _repoAuthor.SelectAuthorDto();

                List<BookTypeDto> bookTypes = _repoBookType.SelectBookTypeDto();
                return View((book, authors, bookTypes));
            }
            _repoBook.Add(book);
            return RedirectToAction("BookList", "Book", new { area = "Management" });
        }
    }
}
