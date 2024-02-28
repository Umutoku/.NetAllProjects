using System;
using System.IO;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionQueueTrigger
{
    public class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }

    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([QueueTrigger("picturesqueue", Connection = "AzureWebJobsStorage")] string message,
        //Product product,
        //CloudQueueMessage product, // The function processes a message from a queue.
            ILogger log,[Blob("pictures/{queuemessage}",System.IO.FileAccess.Read)]CloudBlockBlob cloudBlockBlob) // The BlockBlob is bound to the queue message.
        {
            log.LogInformation($"C# Queue trigger function processed: {cloudBlockBlob.Name}");


        }
    }
}
