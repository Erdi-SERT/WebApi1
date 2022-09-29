using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Builder;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using WebApi1.Services;

namespace WebApi1.Middleweare
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;
        public CustomExceptionMiddleware(RequestDelegate next,ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService; 
        }

        public object JsonConvertor { get; private set; }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();

            try
            {
                string messgae = "[Request] HTTP " + context.Request.Method + "-" + context.Request.Path;
                _loggerService.Write(messgae);
                await _next(context);

                watch.Stop();
                messgae = "[Response] HTTP" + context.Request.Method + "-" + context.Request.Path + " responded " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + "ms";
                _loggerService.Write(messgae);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
                throw;
            }
            
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            string message = "[Error]   HTTP" +context.Request.Method+"-"+context.Response.StatusCode+"Error Message:"+ex.Message+"in "+watch.Elapsed.TotalMilliseconds+"ms";
            _loggerService.Write(message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var result=JsonConvert.SerializeObject(new 
            {
                error=ex.Message,

            },Formatting.None);
            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
