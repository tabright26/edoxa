// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-11-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Application.Cors.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomCors(this IApplicationBuilder application)
        {
            var configuration = application.ApplicationServices.GetRequiredService<IConfiguration>();

            if (configuration?.GetValue<bool>("Kubernetes_Enabled") ?? false)
            {
                return application.UseCors("Kubernetes");
            }

            return application.UseCors();
        }
    }
}
