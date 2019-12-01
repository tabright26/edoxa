// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-12-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Security.ForwardedHeaders.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IServiceCollection AddCustomForwardedHeaders(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();

            var configuration = provider.GetRequiredService<IConfiguration>();

            return configuration.IsFowardedHeadersEnabled()
                ? services.Configure<ForwardedHeadersOptions>(
                    options =>
                    {
                        options.ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor |
                                                   Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto;

                        // Only loopback proxies are allowed by default.
                        // Clear that restriction because forwarders are enabled by explicit 
                        // configuration.
                        options.KnownNetworks.Clear();
                        options.KnownProxies.Clear();
                    })
                : services;
        }
    }
}
