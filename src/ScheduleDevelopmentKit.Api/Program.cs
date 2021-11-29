using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ScheduleDevelopmentKit.Api;
using ScheduleDevelopmentKit.Api.Infrastructure.Extensions;
using Serilog;

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, configuration) => configuration.Enrich.FromLogContext().WriteTo.Console())
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .AddInfrastructure();