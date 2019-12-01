// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Hellang.Middleware.ProblemDetails;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomPathBase(this IApplicationBuilder application)
        {
            var configuration = application.ApplicationServices.GetRequiredService<IConfiguration>();

            return application.UsePathBase(configuration["ASPNETCORE_PATHBASE"]);
        }

        public static IApplicationBuilder UseCustomMvcOrApiExceptionHandler(this IApplicationBuilder application)
        {
            return application.Use(
                async (context, next) =>
                {
                    if (context.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase))
                    {
                        application.UseProblemDetails();
                    }
                    else
                    {
                        application.UseCustomMvcExceptionHandler();
                    }

                    await next();
                });
        }

        public static IApplicationBuilder UseCustomMvcExceptionHandler(this IApplicationBuilder application)
        {
            if (application.ApplicationServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }
            else
            {
                application.UseExceptionHandler("/Home/Error");
            }

            application.UseStatusCodePages();

            return application;
        }
    }
}
