using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ScheduleDevelopmentKit.Api.Infrastructure.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        private const string MessageTemplate = "HTTP Request {RequestPath}\n{RequestHeaders}";
        private const string MessageWithBodyTemplate = MessageTemplate + "\n{RequestBody}";
                                               
        
        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var builder = new StringBuilder();
            foreach (var (key, value) in context.Request.Headers)
            {
                builder.AppendLine($"{key}:{value}");
            }

            var requestBody = await ReadRequestBody(context.Request);

            if (requestBody is null)
            {
                _logger.LogInformation(MessageTemplate, context.Request.Path, builder.ToString());
            }
            else
            {
                _logger.LogInformation(MessageWithBodyTemplate, context.Request.Path, builder.ToString(), requestBody);
            }

            await _next(context);
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            try
            {
                if (request.ContentLength > 0)
                {
                    request.EnableBuffering();
                    var buffer = new byte[request.ContentLength.Value];
                    await request.Body.ReadAsync(buffer, 0, buffer.Length);
                    var bodyAsText = Encoding.UTF8.GetString(buffer);
                    
                    request.Body.Position = 0;

                    return bodyAsText;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log HTTP request body");
            }

            return null;
        }
    }
}