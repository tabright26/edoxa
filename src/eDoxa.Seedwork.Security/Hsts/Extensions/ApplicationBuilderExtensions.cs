// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-12-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eDoxa.Seedwork.Security.Hsts.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomHsts(this IApplicationBuilder application)
        {
            return application.ApplicationServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment() ? application : application.UseHsts();
        }
    }
}
