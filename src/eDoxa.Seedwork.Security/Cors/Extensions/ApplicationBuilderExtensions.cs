// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-11-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eDoxa.Seedwork.Security.Cors.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomCors(this IApplicationBuilder application)
        {
            var services = application.ApplicationServices;

            var environment = services.GetRequiredService<IWebHostEnvironment>();

            var configuration = services.GetRequiredService<IConfiguration>();

            if (configuration.IsCorsEnabled())
            {
                if (environment.IsProduction())
                {
                    return application.UseCors(Environments.Production);
                }

                return application.UseCors();
            }

            return application;
        }
    }
}
