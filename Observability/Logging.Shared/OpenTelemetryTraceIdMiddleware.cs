using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Shared
{
    public class OpenTelemetryTraceIdMiddleware
    {
        private readonly RequestDelegate _next;

        public OpenTelemetryTraceIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var logger = context.RequestServices.GetService(typeof(ILogger<OpenTelemetryTraceIdMiddleware>)) as ILogger<OpenTelemetryTraceIdMiddleware>;

            using (logger?.BeginScope("{@traceId}", Activity.Current?.TraceId.ToString())) // OpenTelemetry ile gelen trace id'yi loglara ekler
            {
                await _next(context); // Request sonrası middleware'lerin çalışması için
            }
        }
    }
}
