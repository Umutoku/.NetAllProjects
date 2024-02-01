using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observability.ConsoleAPP
{
    internal class ServiceTwo
    {
        internal async Task<int> WriteToFile(string text)
        {
            using var activity = ActivitySourceProvider.source.StartActivity(kind:ActivityKind.Server,name:"WriteToFile");

            await File.WriteAllTextAsync("test.txt", text);

            return await File.ReadAllTextAsync("test.txt").ContinueWith(t => t.Result.Length);
        }

       
    }
}
