using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using WebApp.Command.Commands;
using WebApp.Command.Models;

namespace WebApp.Command.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public ProductsController(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        public async Task<IActionResult> CreateFile(int type)
        {

            var products = await _context.Products.ToListAsync();

            FileCreateInvoker fileCreateInvoker = new FileCreateInvoker();

            EFileType eFileType = (EFileType)type;

            switch (eFileType)
            {
                case EFileType.Excel:
                    ExcelFile<Product> excelFile = new ExcelFile<Product>(products);
                    fileCreateInvoker.SetCommand(new CreateExcelTableActionCommand<Product>(excelFile));
                    break;
                case EFileType.Pdf:
                    PdfFile<Product> pdfFile = new PdfFile<Product>(HttpContext, products);
                    fileCreateInvoker.SetCommand(new CreatePdfTableActionCommand<Product>(pdfFile)); 
                    break;
                default:
                    break;
            }

            return fileCreateInvoker.CreateFile();
        }

        public async Task<IActionResult> CreateFiles()
        {
            var products = await _context.Products.ToListAsync();

            FileCreateInvoker fileCreateInvoker = new FileCreateInvoker();

            ExcelFile<Product> excelFile = new ExcelFile<Product>(products);
            fileCreateInvoker.AddCommand(new CreateExcelTableActionCommand<Product>(excelFile));

            PdfFile<Product> pdfFile = new PdfFile<Product>(HttpContext, products);
            fileCreateInvoker.AddCommand(new CreatePdfTableActionCommand<Product>(pdfFile));

            var result = fileCreateInvoker.CreateFiles();

            using(var zipMemoryStream = new MemoryStream()) // using içerisinde tanımlanmış MemoryStream, using bitince otomatik olarak dispose edilir.
            {
                using (var zipArchive = new ZipArchive(zipMemoryStream, ZipArchiveMode.Create, true)) // using içerisinde tanımlanmış ZipArchive, using bitince otomatik olarak dispose edilir. ziparchive nedir diye sorarsak, zip dosyası oluşturmak için kullanılır.
                {
                    foreach (var item in result)
                    {
                        var fileContent = item as FileContentResult;

                        var entry = zipArchive.CreateEntry(fileContent.FileDownloadName);
                        using (var entryStream = entry.Open()) 
                        {
                           await new MemoryStream(fileContent.FileContents).CopyToAsync(entryStream); // burada fileContent.FileContents MemoryStream tipinde olduğu için, MemoryStream tipindeki bir nesnenin CopyToAsync metodunu kullanabiliyoruz.

                            //var fileStream = new MemoryStream();
                            //item.FileStream.CopyTo(fileStream);
                            //fileStream.Position = 0;
                            //fileStream.CopyTo(entryStream);
                        }
                    }
                }
                zipMemoryStream.Position = 0;
                return File(zipMemoryStream, "application/zip", "files.zip");
            }
           
        }
    }
}

