using System;
using System.IO;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionTimerTrigger
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([TimerTrigger("* * * * * *")]TimerInfo myTimer, ILogger log, [Blob("log/{DateTime}.txt",System.IO.FileAccess.Write,Connection ="MyAzureStorage")] Stream stream) // The function writes the current date and time to a blob every second.
        {

            var ifade = Encoding.UTF8.GetBytes($"C# Timer trigger function executed at: {DateTime.Now}");

            stream.Write(ifade, 0, ifade.Length); // The function writes the current date and time to a blob every second.

            stream.Close(); // The function closes the stream.

            stream.Dispose(); // The function disposes the stream.

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
