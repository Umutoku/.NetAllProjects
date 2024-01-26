using Elasticsearch.Web.Models;
using Elasticsearch.Web.Services;
using Elasticsearch.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Search()
        {
            return View(await _blogService.SearchAsync(string.Empty));
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchText)
        {
            ViewBag.searchText = searchText;
			var result = await _blogService.SearchAsync(searchText);
			return View(result);
		}

        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(BlogCreateViewModel blogCreateViewModel)
        {
            var result = await _blogService.SaveAsync(blogCreateViewModel);

            if (!result)
            {
                TempData["Result"] = "Error while saving blog";
                return RedirectToAction(nameof(BlogController.Save));
            }
            TempData["Result"] = "Blog saved successfully";
            return RedirectToAction(nameof(BlogController.Save));
        }
    }
}
