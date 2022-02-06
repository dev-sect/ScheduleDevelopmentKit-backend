using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ScheduleDevelopmentKit.Api.Infrastructure.Middlewares;
using ScheduleDevelopmentKit.DataAccess;
using ScheduleDevelopmentKit.Modules.Core;
using Serilog;

namespace ScheduleDevelopmentKit.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            services.AddDbContext<SdkDbContext>(options =>
                                                    options.UseNpgsql(connectionString));
            services.AddControllers(options => options.Filters.Add(new GlobalExceptionFilter()));
            services.AddCoreModule();
            services.AddOptions();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}