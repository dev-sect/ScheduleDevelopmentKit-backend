using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScheduleDevelopmentKit.Api.Infrastructure.Middlewares;
using ScheduleDevelopmentKit.DataAccess;
using ScheduleDevelopmentKit.Modules.Core;

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
            services.AddDbContext<SdkDbContext>(options =>
                 options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers(options => options.Filters.Add(new GlobalExceptionFilter()));
            services.AddCoreModule();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}