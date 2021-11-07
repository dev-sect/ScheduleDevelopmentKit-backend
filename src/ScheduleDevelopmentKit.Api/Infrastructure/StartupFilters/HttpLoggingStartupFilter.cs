using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using ScheduleDevelopmentKit.Api.Infrastructure.Middlewares;

namespace ScheduleDevelopmentKit.Api.Infrastructure.StartupFilters
{
    public class HttpLoggingStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseMiddleware<RequestLoggingMiddleware>();
                app.UseMiddleware<ResponseLoggingMiddleware>();
                
                next(app);
            };
        }
    }
}