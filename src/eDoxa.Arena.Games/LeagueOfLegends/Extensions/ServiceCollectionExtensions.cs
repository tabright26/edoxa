// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Net;

using eDoxa.Arena.Games.LeagueOfLegends.Abstractions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Polly;
using Polly.Extensions.Http;

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
                .AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError()
                    .OrResult(message => message.StatusCode == HttpStatusCode.NotFound)
                    .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
                .AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError().CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
        }
    }
}
