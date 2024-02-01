using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observability.ConsoleAPP
{
    internal class ServiceHelperTwo
    {

        public void Work2()
        {
            using var activity = ActivitySourceProvider.SourceFile.StartActivity();

            activity?.SetTag("ServiceName", OpenTelemetryConstants.ServiceName);
            activity?.AddEvent(new ActivityEvent("Work2 başladı"));

            Console.WriteLine("Work2 tamamlandı");

        }

    }
}
