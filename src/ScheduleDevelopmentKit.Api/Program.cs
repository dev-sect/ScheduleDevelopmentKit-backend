using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ScheduleDevelopmentKit.Api;
using ScheduleDevelopmentKit.Api.Infrastructure.Extensions;

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .AddInfrastructure();

