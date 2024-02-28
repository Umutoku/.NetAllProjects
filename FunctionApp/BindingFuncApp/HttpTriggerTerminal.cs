using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Tables;
using Microsoft.WindowsAzure.Storage.Table;
using BindingFuncApp;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Company.Function
{
    public static class HttpTriggerTerminal
    {
        [FunctionName("HttpTriggerTerminal")]
        [return: Table("products", Connection = "MyAzureStorage")] // Bu kod ile products adında bir tablo oluşturduk ve bu tabloya bağlandık. Buradaki bind işlemi ile attributes alarak tabloya bağlandık.
        internal static async Task<Product> Run
            (
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log,
            //[Table("products",Connection = "MyAzureStorage")] CloudTable cloudTable, 
            [Queue("products", Connection = "MyAzureStorage")] CloudQueue cloudQueue
            ) // Bu kod ile products adında bir tablo oluşturduk ve bu tabloya bağlandık. Buradaki bind işlemi ile attributes alarak tabloya bağlandık.
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync(); // Gelen isteği okuduk.


            //await cloudTable.CreateIfNotExistsAsync(); // Tablo yoksa oluşturulur.

            //TableOperation insertOperation = TableOperation.Insert(newProduct); // Yeni bir ürün eklemek için insert işlemi oluşturduk.

            //TableResult insertResult = await cloudTable.ExecuteAsync(insertOperation); // insert işlemini gerçekleştirdik.


            await cloudQueue.CreateIfNotExistsAsync(); // Kuyruk yoksa oluşturulur.

            Product newProduct = JsonConvert.DeserializeObject<Product>(requestBody);
            var productMessage = JsonConvert.SerializeObject(newProduct); // Ürünü JSON formatına çevirdik.

            CloudQueueMessage cloudQueueMessage = new CloudQueueMessage(productMessage); // Ürünü kuyruğa eklemek için bir mesaj oluşturduk.

            await cloudQueue.AddMessageAsync(cloudQueueMessage); // Kuyruğa mesajı ekledik.

            //return new OkObjectResult(insertResult.Result); // Sonucu döndürdük.

            return newProduct;

        }
    }
}
