using System;
using Microsoft.Azure.WebJobs;
using AzureStorageLibrary.Models;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Drawing;
using AzureStorageLibrary;
using AzureStorageLibrary.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;

namespace WatermarkProcessFunction
{
    public class Function1
    {
        [FunctionName("Function1")]
        public async static Task Run([QueueTrigger("watermark-queue")] PictureWatermarkQueue myQueueItem, ILogger log)
        {
            ConnectionStrings.AzureStorageConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            IBlobStorage blobStorage = new BlobStorage();

            INoSqlStorage<UserPictures> noSqlStorage = new TableStorage<UserPictures>();

            foreach (var picture in myQueueItem.Pictures)
            {
                var pictureStream = await blobStorage.DownloadAsync(picture, EContainerName.Pictures);
                var watermarkedStream = AddWatermark(myQueueItem.WatermarkText, pictureStream); // resme watermark ekle ve memory stream olarak döner
                await blobStorage.UploadAsync(watermarkedStream, picture, EContainerName.Pictures);

                log.LogInformation($"Watermark is added to {picture}");
            }

            var userPictures = await noSqlStorage.Get(new UserPictures { PartitionKey = myQueueItem.UserId, RowKey = myQueueItem.City }); // kullanıcının resimlerini getirir

            if(userPictures.WatermarkRawPaths!=null)
            {
                myQueueItem.Pictures.AddRange(userPictures.WatermarkPaths); // kullanıcının resimlerine watermark eklenmiş resimleri ekler
            }
            
            userPictures.WatermarkPaths = myQueueItem.Pictures; // kullanıcının resimlerini tutar

            await noSqlStorage.Add(userPictures); // kullanıcının resimlerini ekler

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync($"https://localhost:44300/api/notification/{myQueueItem.ConnectionId}"); // kullanıcıya sinyal gönderir

            log.LogInformation($"C# Queue trigger function processed: {myQueueItem.Pictures.Count} items at: {DateTime.Now}");

        }

        public static MemoryStream AddWatermark(string watermarkText, Stream pictureStream)
        { 
            MemoryStream watermarkedStream = new MemoryStream();
            using (Image bitmap = Bitmap.FromStream(pictureStream))
            { 
                using(Bitmap tempBitmap = new Bitmap(bitmap.Width,bitmap.Height))
                {
                    using(Graphics gph = Graphics.FromImage(tempBitmap))
                    {
                        gph.DrawImage(bitmap, 0, 0); // resmi tempBitmap'e çiz
                        Font font = new Font("Arial", 20);
                        SolidBrush brush = new SolidBrush(Color.Red);
                        gph.DrawString(watermarkText, font, brush, new PointF(bitmap.Width / 2, bitmap.Height / 2)); // resmin ortasına yazıyı çiz
                        tempBitmap.Save(watermarkedStream, bitmap.RawFormat);
                    }
                }
            }

            watermarkedStream.Position = 0; // memory streami sıfırla, alacak kişi baştan okusun

            return watermarkedStream;
        }
    }
}
