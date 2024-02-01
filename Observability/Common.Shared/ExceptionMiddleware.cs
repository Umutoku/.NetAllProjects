using Common.Shared.DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shared
{
    public static class ExceptionMiddleware
    {
        public static void ExceptionMiddlewareLog(this WebApplication application)
        {
            application.UseExceptionHandler(cfg =>
            {

                cfg.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();

                    var exception = exceptionHandlerPathFeature!.Error;

                    var response = ResponseDto<object>.Fail(500, exception.Message);

                    //var response = new
                    //{
                    //    StatusCode = context.Response.StatusCode,
                    //    Message = exception.Message,
                    //    StackTrace = exception.StackTrace
                    //};

                    await context.Response.WriteAsJsonAsync(response.ToString());
                });

            });
        }
    }
}
