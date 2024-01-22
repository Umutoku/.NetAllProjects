using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observability.ConsoleAPP
{
    internal class ServiceHelper
    {

        internal async Task Work1()
        {
            using var activity = ActivitySourceProvider.source.StartActivity();


            var serviceOne = new ServiceOne();

            var result = await serviceOne.MakeRequestToGoogle();

            Console.WriteLine($"Google'dan gelen veri uzunluğu: {result}");

            Console.WriteLine("Work1 tamamlandı");

        }
    }
}
