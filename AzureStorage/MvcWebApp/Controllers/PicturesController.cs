using AzureStorageLibrary;
using AzureStorageLibrary.Models;
using AzureStorageLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using MvcWebApp.Models;
using Newtonsoft.Json;
using System.Text;

namespace MvcWebApp.Controllers
{
    public class PicturesController : Controller
    {
        public string UserId { get; set; } = "123";
        public string City { get; set; } = "Ankara";
        private readonly INoSqlStorage<UserPictures> _noSqlStorage;
        private readonly IBlobStorage _blobStorage;

        public PicturesController(INoSqlStorage<UserPictures> noSqlStorage, IBlobStorage blobStorage)
        {
            _noSqlStorage = noSqlStorage;
            _blobStorage = blobStorage;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.UserId = UserId;
            ViewBag.City = City;

            List<FileBlob> fileBlobs = new List<FileBlob>();
            var userPictures= new UserPictures { PartitionKey = UserId, RowKey = City }; // kullanıcı resimlerini tutar

            var user = await _noSqlStorage.Get(userPictures);

            ViewBag.BlobUrl = $"{_blobStorage.BlobUrl}/{EContainerName.Pictures}"; // blobun url'sini viewbag'e atar

            if (user != null)
            {
                user.Paths.ForEach(x =>
                {
                    fileBlobs.Add(new FileBlob { Name = x, Url = $"{ViewBag.BlobUrl}/{x}" });
                });
            }
            ViewBag.FileBlobs = fileBlobs;


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IEnumerable<IFormFile> pictures)
        {
            List<string> paths = new List<string>();
            foreach (var file in pictures)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}"; 

                await _blobStorage.UploadAsync(file.OpenReadStream(), fileName, EContainerName.Pictures);
                paths.Add(fileName);
            }

            var isUser = await _noSqlStorage.Get(new UserPictures { PartitionKey = UserId, RowKey = City }); // kullanıcı resimlerini getirir

            if (isUser != null)
            {
                paths.AddRange(isUser.Paths); // kullanıcının resimlerini ekler
                isUser.Paths = paths; // kullanıcının resimlerini tutar

            }
            else
            {
                isUser = new UserPictures { PartitionKey = UserId, RowKey = City, Paths = paths }; // kullanıcı resimlerini tutar
            }

            await _noSqlStorage.Add(isUser); // kullanıcı resimlerini ekler



            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShowWatermark()
        { 
            UserPictures userPictures =await _noSqlStorage.Get(new UserPictures { PartitionKey = UserId, RowKey = City }); // kullanıcı resimlerini getirir

            List<FileBlob> fileBlobs = new List<FileBlob>();

            userPictures.WatermarkPaths.ForEach(x =>
            {
                fileBlobs.Add(new FileBlob { Name = x, Url = $"{_blobStorage.BlobUrl}/{EContainerName.Watermark}/{x}" });
            });

            ViewBag.FileBlobs = fileBlobs;

            return View();
        }

        public async Task<IActionResult> AddWatermark(PictureWatermarkQueue picture)
        { 
            var jsonString = JsonConvert.SerializeObject(picture); // picture nesnesini json stringe çevirir

            string jsonStringBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonString)); // json stringi base64'e çevirir

            AzureQueue azureQueue = new AzureQueue("watermark-queue"); // azure queue nesnesi oluşturur
            await azureQueue.SendMessageAsync(jsonStringBase64, default); // json stringi base64'e çevirir

            return RedirectToAction("Index");
        }


    }
}
