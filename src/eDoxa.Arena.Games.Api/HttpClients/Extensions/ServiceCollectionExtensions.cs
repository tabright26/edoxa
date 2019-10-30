// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-10-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Arena.Games.Api.Infrastructure;
using eDoxa.Seedwork.Application.DelegatingHandlers;

using Microsoft.Extensions.DependencyInjection;

using Refit;

namespace eDoxa.Arena.Games.Api.HttpClients.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHttpClients(this IServiceCollection services, GamesAppSettings appSettings)
        {
            services.AddTransient<AccessTokenDelegateHandler>();

            services.AddRefitClient<IGatewayHttpClient>()
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(appSettings.HttpClients.Web.GatewayUrl))
                .AddHttpMessageHandler<AccessTokenDelegateHandler>();
        }
    }
}
