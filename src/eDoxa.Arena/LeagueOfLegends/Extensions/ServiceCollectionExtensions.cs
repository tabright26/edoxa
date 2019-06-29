﻿// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-06-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.LeagueOfLegends.Abstractions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Arena.LeagueOfLegends.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static void AddLeagueOfLegends(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LeagueOfLegendsOptions>(options => options.RiotToken = configuration["Arena:LeagueOfLegends:ApiKey"]);
            services.AddTransient<LeagueOfLegendsDelegatingHandler>();

            // TODO: Should be injected from integration tests.
            services.AddTransient<ILeagueOfLegendsService, FakeLeagueOfLegendsService>();
            // TODO: Must adding HttpPolicies.
            //services.AddHttpClient<ILeagueOfLegendsService, LeagueOfLegendsService>().AddHttpMessageHandler<LeagueOfLegendsDelegatingHandler>();
            services.AddSingleton<ILeagueOfLegendsProxy, LeagueOfLegendsProxy>();
        }
    }
}
