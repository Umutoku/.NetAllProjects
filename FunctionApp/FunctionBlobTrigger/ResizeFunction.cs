using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace FunctionBlobTrigger
{
    public class ResizeFunction
    {
        [FunctionName("ResizeFunction")]
        public async Task Run([BlobTrigger("pictures/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob, string name, ILogger log,
             //[Blob("pictures-resize/{name}",FileAccess.Write,Connection = "AzureWebJobsStorage")] Stream outputBlobStream ,
             [Blob("pictures-resize", Connection = "AzureWebJobsStorage")] CloudBlobContainer container // stream yerine CloudBlobContainer kullanıldı.
             )
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            await container.CreateIfNotExistsAsync(); // The function creates the container if it does not exist.

            var format = await Image.DetectFormatAsync(myBlob); // The function detects the format of the image.

            var blockBlob = container.GetBlockBlobReference(name + "." + format.FileExtensions.First()); // The function gets the block blob reference and sets the name of the blob.

            var resizeImage = Image.Load(myBlob); // The function loads the image.

            resizeImage.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(100, 100), // The function resizes the image to 100x100 pixels.
                Mode = ResizeMode.Max
            }));

            using (var outputBlobStream = await blockBlob.OpenWriteAsync()) // The function opens the output blob stream.
            {
                resizeImage.Save(outputBlobStream, format); // The function saves the resized image to the output blob stream.

                outputBlobStream.Position = 0; // The function sets the position of the output blob stream to the beginning.

                await blockBlob.UploadFromStreamAsync(outputBlobStream); // The function uploads the output blob stream to the block blob.

                log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {outputBlobStream.Length} Bytes"); // The function logs the name and size of the output blob stream.

            }

            //resizeImage.Save(outputBlobStream, format); // The function saves the resized image to the output blob stream.

        }
    }
}
