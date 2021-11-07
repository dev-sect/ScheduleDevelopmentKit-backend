using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using ScheduleDevelopmentKit.Api.Infrastructure.Middlewares;

namespace ScheduleDevelopmentKit.Api.Infrastructure.StartupFilters
{
    public class TerminalStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.Map("/version", builder => builder.UseMiddleware<VersionMiddleware>());
                app.Map("/live", builder => builder.UseMiddleware<LiveMiddleware>());
                
                next(app);
            };
        }
    }
}