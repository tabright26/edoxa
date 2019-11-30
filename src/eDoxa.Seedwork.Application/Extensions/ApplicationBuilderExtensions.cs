// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-09-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomPathBase(this IApplicationBuilder application)
        {
            var configuration = application.ApplicationServices.GetRequiredService<IConfiguration>();

            return application.UsePathBase(configuration["ASPNETCORE_PATHBASE"]);
        }
    }
}
