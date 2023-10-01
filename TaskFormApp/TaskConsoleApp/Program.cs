using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TaskConsoleApp
{


    internal class Program
    {
        public static int cachedata { get; set; } = 150;
        private async static Task Main(string[] args)
        {
            Console.WriteLine("ilk adım");
            var mytask =GetContent();
            Console.WriteLine("ikinci adım");
            await mytask;
            Console.WriteLine("üçüncü adım");

        }

        public static async Task<string> GetContent()
        {
            return await new HttpClient().GetStringAsync("https://www.google.com");
            
        }


    }
}


