using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FirstFunction
{
    public class MyHttpTrigger
    {
        private readonly IService _service;

        public MyHttpTrigger(IService service)
        {
            _service = service;
        }

        [FunctionName("MyHttpTrigger")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log) // Ilogger burada DI ile geliyor
        {
            string myApıKey = Environment.GetEnvironmentVariable("MyApi"); // Environment.GetEnvironmentVariable ile Azure Function App üzerindeki Application Settings'ten değer okunabilir

            log.LogInformation(_service.Write()); // IService interface'ini kullanarak Service class'ındaki Write methodunu çağırıyoruz

            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync(); // request body okunuyor
            // dynamic tipi runtime'da belirlenen bir tiptir. Var tipinden farklı olarak dynamic tipi compile time'da değil runtime'da belirlenir.
            dynamic data = JsonConvert.DeserializeObject(requestBody); // request body json formatında olduğu için json formatına çeviriyoruz

            return new OkObjectResult($"{data.id} - {data.Name}"); // response olarak id ve name döndürüyoruz

            //Product product = JsonConvert.DeserializeObject<Product>(requestBody); // request body json formatında olduğu için json formatına çeviriyoruz

            //string name = req.Query["name"];

            
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            //string responseMessage = string.IsNullOrEmpty(name)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";

            //return new OkObjectResult(product);
        }

        [FunctionName("MyHttpTrigger2")]
        public static async Task<IActionResult> RunById(
    [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "products/{id}")] HttpRequest req,
    ILogger log,int id)
        {
            log.LogInformation("Gelen id :" + id);

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
