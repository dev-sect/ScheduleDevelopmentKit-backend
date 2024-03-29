﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ScheduleDevelopmentKit.Api.Infrastructure.StartupFilters;

namespace ScheduleDevelopmentKit.Api.Infrastructure.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartupFilter, TerminalStartupFilter>();
                services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();

                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo {Title = "ScheduleDevelopmentKit.Api", Version = "v1"});
                    
                    options.CustomSchemaIds(selector =>
                    {
                        var type = selector;
                        var typeNames = new List<string> {type.Name};

                        while (type?.IsNested ?? false)
                        {
                            type = type.DeclaringType;
                            typeNames.Add(type?.Name);
                        }

                        typeNames.Reverse();
                        return string.Join(".", typeNames);
                    });
                });
            });
            
            return builder;
        }
    }
}