// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-06-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Api.Games.LeagueOfLegends.Abstractions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Arena.Challenges.Api.Games.LeagueOfLegends.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static void AddLeagueOfLegendsServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LeagueOfLegendsOptions>(options => options.RiotToken = configuration["Arena:LeagueOfLegends:ApiKey"]);
            services.AddTransient<LeagueOfLegendsDelegatingHandler>();
            // TODO: Must adding HttpPolicies.
            services.AddHttpClient<ILeagueOfLegendsService, LeagueOfLegendsService>().AddHttpMessageHandler<LeagueOfLegendsDelegatingHandler>();
            services.AddSingleton<ILeagueOfLegendsProxy, LeagueOfLegendsProxy>();
        }
    }
}
