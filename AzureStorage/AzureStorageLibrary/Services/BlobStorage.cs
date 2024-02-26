using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageLibrary.Services
{
    public class BlobStorage : IBlobStorage
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobStorage()
        {
            _blobServiceClient = new BlobServiceClient(ConnectionStrings.AzureStorageConnectionString); 
        }

        public string BlobUrl => _blobServiceClient.Uri.ToString(); // blobun urlini döner

        public Task DeleteAsync(string name, EContainerName eContainerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(eContainerName.ToString().ToLower());
            var blobClient = containerClient.GetBlobClient(name);
            return blobClient.DeleteIfExistsAsync();
        }

        public async Task<Stream> DownloadAsync(string name, EContainerName eContainerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(eContainerName.ToString().ToLower());
            var blobClient = containerClient.GetBlobClient(name);
            var blobDownloadInfo = await blobClient.DownloadAsync(); // blobu indirir
            var filePath = $@"C:\Users\{Environment.UserName}\Downloads\{name}"; // indirilen blobun kaydedileceği yeri belirler
            await blobDownloadInfo.Value.Content.CopyToAsync(new System.IO.FileStream(filePath, System.IO.FileMode.Create)); // blobu indirilen yere kaydeder

            return new System.IO.FileStream(filePath, System.IO.FileMode.Open); // indirilen blobun streamini döner
        }

        public async Task<List<string>> GetLogAsync(string name)
        {
            List<string> logs = new List<string>();

            var containerClient = _blobServiceClient.GetBlobContainerClient(EContainerName.Log.ToString().ToLower());
            var appendBlobClient = containerClient.GetAppendBlobClient(name); // sayesinde appendblob oluşturur. appendbloblar üzerine yazma yapılabilir
            await appendBlobClient.CreateIfNotExistsAsync(); // eğer appendblob yoksa oluşturur

            var info = await appendBlobClient.OpenReadAsync(); // appendblobu okur
            var reader = new System.IO.StreamReader(info); // okunan appendblobu okur

            string line;
            while ((line = reader.ReadLine()) != null) // appendblobun sonuna kadar okur
            {
                logs.Add(line);
            }
            reader.Close();  // dispose işlemi
            return logs;
        }

        public async Task<List<string>> GetName(EContainerName eContainerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(eContainerName.ToString().ToLower());
            List<string> names = new List<string>();
            await foreach(var blobItem in containerClient.GetBlobsAsync())
            {
                names.Add(blobItem.Name);
            }
            return names;
        }

        public async Task SetLogAsync(string text, string name)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(EContainerName.Log.ToString().ToLower());

            var appendBlobClient = containerClient.GetAppendBlobClient(name);
            await appendBlobClient.CreateIfNotExistsAsync(); // eğer appendblob yoksa oluşturur

            var ms = new MemoryStream(); // texti byte dizisine çevirir

            var writer = new StreamWriter(ms);

            writer.WriteLine(text); // texti yazar

            writer.Flush(); // belleği temizler

            ms.Position = 0; // memory streamin başlangıcına döner


            await appendBlobClient.AppendBlockAsync(ms); // appendbloba text ekler

            writer.Close(); // dispose işlemi
        }

        public async Task UploadAsync(Stream fileStream, string name, EContainerName eContainerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(eContainerName.ToString().ToLower()); //  bu kod saqyede container oluştururken adı küçük harf yapmamızı sağlar
            await containerClient.CreateIfNotExistsAsync();

            await containerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer); // bu sayede container içindeki tüm bloblar public olur

            var blobClient = containerClient.GetBlobClient(name); // blobun adını belirler

            await blobClient.UploadAsync(fileStream, true); // blobu yükler, true yazarsak blobu overwrite eder

        }
    }
}
