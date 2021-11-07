using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OzonEdu.MerchandiseService.Infrastructure.Filters;
using ScheduleDevelopmentKit.Api.Infrastructure.StartupFilters;

namespace ScheduleDevelopmentKit.Api.Infrastructure.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());
                
                services.AddSingleton<IStartupFilter, TerminalStartupFilter>();
                services.AddSingleton<IStartupFilter, HttpLoggingStartupFilter>();
                services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();

                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo {Title = "ScheduleDevelopmentKit.Api", Version = "v1"});
                });
            });
            
            return builder;
        }
    }
}