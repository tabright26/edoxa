// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-11-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Challenges.Api.Infrastructure;
using eDoxa.Seedwork.Application.Refit.DelegatingHandlers;

using Microsoft.Extensions.DependencyInjection;

using Refit;

namespace eDoxa.Challenges.Api.HttpClients.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHttpClients(this IServiceCollection services, ChallengesAppSettings appSettings)
        {
            services.AddTransient<AccessTokenDelegatingHandler>();

            services.AddRefitClient<ICashierHttpClient>()
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(appSettings.Endpoints.CashierUrl))
                .AddHttpMessageHandler<AccessTokenDelegatingHandler>();

            services.AddRefitClient<IGamesHttpClient>()
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(appSettings.Endpoints.GamesUrl))
                .AddHttpMessageHandler<AccessTokenDelegatingHandler>();
        }
    }
}
