using AuthService.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace AuthService.Wrapper
{
    public class ResponseWrapper
    {
        private readonly RequestDelegate _next;

        public ResponseWrapper(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var currentBody = context.Response.Body;

            using (var memoryStream = new MemoryStream())
            {
                 context.Response.Body = memoryStream;
                try
                {
                    await _next(context);
                    await HandleExceptionAsync(context, currentBody, memoryStream, Messages.Successfully);
                }
                catch (ApplicationException avEx)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await HandleExceptionAsync(context, currentBody, memoryStream, avEx.Message);
                }
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Stream currentBody, MemoryStream memoryStream, string message)
        {
            context.Response.Body = currentBody;
            context.Response.ContentType = "application/json";
            memoryStream.Seek(0, SeekOrigin.Begin);

            var readToEnd = new StreamReader(memoryStream).ReadToEnd();
            try
            {
                var objResult = JsonConvert.DeserializeObject(readToEnd);
                if (context.Response.StatusCode == 405)
                {
                    message = Messages.Error405;
                }
                if (context.Response.StatusCode == 400)
                {
                    message = Messages.Error400;
                }
                if (context.Response.StatusCode == 404)
                {
                    message = Messages.Error404;
                }
                var result = CommonApiResponse.Create((HttpStatusCode)context.Response.StatusCode, objResult, message);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                var result = CommonApiResponse.Create((HttpStatusCode)context.Response.StatusCode, null, ex.Message);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }
    }


    public static class ResponseWrapperExtensions
    {
        public static IApplicationBuilder UseResponseWrapper(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseWrapper>();
        }
    }


    public class CommonApiResponse
    {
        public static CommonApiResponse Create(HttpStatusCode statusCode, object result = null, string errorMessage = null)
        {
            return new CommonApiResponse(statusCode, result, errorMessage);
        }

        public int StatusCode { get; set; }

        public string ErrorMessage { get; set; }

        public object Result { get; set; }

        protected CommonApiResponse(HttpStatusCode statusCode, object result = null, string errorMessage = null)
        {
            StatusCode = (int)statusCode;
            Result = result;
            ErrorMessage = errorMessage;
        }
    }
}
