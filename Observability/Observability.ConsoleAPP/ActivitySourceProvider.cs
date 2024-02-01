using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observability.ConsoleAPP
{
    internal static class ActivitySourceProvider
    {
        public static ActivitySource source = new(OpenTelemetryConstants.ActivitySourceName);
        public static ActivitySource SourceFile = new ActivitySource(OpenTelemetryConstants.ActivitySourceFileName);

    }
}
