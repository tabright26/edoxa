// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-11-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eDoxa.Seedwork.Security.Cors.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            return services.AddCors(
                options =>
                {
                    options.AddDefaultPolicy(
                        builder => builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(isOriginAllowed => true));

                    options.AddPolicy(
                        Environments.Production,
                        builder => builder.AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithOrigins("https://*.edoxa.gg")
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowCredentials());
                });
        }
    }
}
