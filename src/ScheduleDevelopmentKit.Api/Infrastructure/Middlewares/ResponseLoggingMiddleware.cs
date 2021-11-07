using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ScheduleDevelopmentKit.Api.Infrastructure.Middlewares
{
    public class ResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseLoggingMiddleware> _logger;

        private const string MessageTemplate = "HTTP Response {ResponsePath}\n{ResponseHeaders}";
        private const string MessageWithBodyTemplate = MessageTemplate + "\n{ResponseBody}";
                                               
        
        public ResponseLoggingMiddleware(RequestDelegate next, ILogger<ResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                var builder = new StringBuilder("\n");
                foreach (var (key, value) in context.Response.Headers)
                {
                    builder.AppendLine($"{key}:{value}");
                }
                
                var responseBody = await ReadResponseBody(context.Response);

                if (responseBody is null)
                {
                    _logger.LogInformation(MessageTemplate, context.Request.Path, builder.ToString());
                }
                else
                {
                    _logger.LogInformation(MessageWithBodyTemplate, context.Request.Path, builder.ToString(), responseBody);
                }
            }
        }
        
        private async Task<string> ReadResponseBody(HttpResponse response)
        {
            try
            {
                if (response.ContentLength > 0)
                {
                    var buffer = new byte[response.ContentLength.Value];
                    await response.Body.ReadAsync(buffer, 0, buffer.Length);
                    var bodyAsText = Encoding.UTF8.GetString(buffer);
                    
                    response.Body.Position = 0;

                    return bodyAsText;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log HTTP response body");
            }

            return null;
        }
    }
}