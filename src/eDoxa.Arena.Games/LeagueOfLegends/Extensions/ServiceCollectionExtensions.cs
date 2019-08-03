// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Games.LeagueOfLegends.Abstractions;
using eDoxa.Seedwork.Monitoring;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Arena.Games.LeagueOfLegends.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static void AddLeagueOfLegends(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LeagueOfLegendsOptions>(options => options.RiotToken = configuration["Arena:LeagueOfLegends:ApiKey"]);

            services.AddTransient<LeagueOfLegendsDelegatingHandler>();

            services.AddHttpClient<ILeagueOfLegendsService, LeagueOfLegendsService>()
                .AddHttpMessageHandler<LeagueOfLegendsDelegatingHandler>()
                .AddPolicyHandler(HttpPolicies.GetRetryPolicy())
                .AddPolicyHandler(HttpPolicies.GetCircuitBreakerPolicy());

            services.AddSingleton<ILeagueOfLegendsProxy, LeagueOfLegendsProxy>();
        }
    }
}
