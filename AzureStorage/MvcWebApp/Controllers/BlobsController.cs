using AzureStorageLibrary;
using Microsoft.AspNetCore.Mvc;
using MvcWebApp.Models;

namespace MvcWebApp.Controllers
{
    public class BlobsController : Controller
    {
        private readonly IBlobStorage _blobStorage;

        public BlobsController(IBlobStorage blobStorage)
        {
            _blobStorage = blobStorage;
        }
        public async Task<IActionResult> Index()
        {
            var names = await _blobStorage.GetName(EContainerName.Pictures);
            names.Select(x=> 
            new FileBlob { Name = x, Url = $"{_blobStorage.BlobUrl}/{EContainerName.Pictures.ToString().ToLower()}/{x}" }).ToList();
            ViewBag.logs = await _blobStorage.GetLogAsync("log.txt"); // log dosyasını okur ve viewbag'e atar
            ViewBag.BlobUrl =  _blobStorage.BlobUrl;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile picture)
        {
            await _blobStorage.SetLogAsync($"Blob {picture.FileName} uploaded", "log.txt"); // log dosyasına yazar

            if (picture != null)
            {
                var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(picture.FileName)}"; // blobun ismini değiştirir ve uzantısını alır
                using (var stream = picture.OpenReadStream()) // resmi okur
                {
                    await _blobStorage.UploadAsync(stream, newFileName, EContainerName.Pictures);
                }
            }
            await _blobStorage.SetLogAsync($"Blob {picture.FileName} uploaded done", "log.txt");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Download(string name)
        {
            await _blobStorage.DownloadAsync(name, EContainerName.Pictures);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string name)
        {
            await _blobStorage.DeleteAsync(name, EContainerName.Pictures);
            return RedirectToAction("Index");
        }
    }
}
