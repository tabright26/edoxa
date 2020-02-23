// Filename: IdentityServerConfig.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Application;
using eDoxa.Swagger.Extensions;

using IdentityServer4;
using IdentityServer4.Models;

using Microsoft.Extensions.Hosting;

using IdentityResources = IdentityServer4.Models.IdentityResources;

namespace eDoxa.Identity.Api.Infrastructure
{
    public static class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            yield return new IdentityResources.OpenId();

            yield return new IdentityResources.Profile();

            yield return new IdentityResources.Email();

            yield return new IdentityResources.Phone();

            yield return new IdentityResources.Address();

            yield return Seedwork.Application.IdentityResources.Country;

            yield return Seedwork.Application.IdentityResources.Roles;

            yield return Seedwork.Application.IdentityResources.Permissions;

            yield return Seedwork.Application.IdentityResources.Games;

            yield return Seedwork.Application.IdentityResources.Stripe;
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            yield return ApiResources.IdentityApi;

            yield return ApiResources.PaymentApi;

            yield return ApiResources.CashierApi;

            yield return ApiResources.NotificationsApi;

            yield return ApiResources.ChallengesApi;

            yield return ApiResources.GamesApi;

            yield return ApiResources.ClansApi;

            yield return ApiResources.ChallengesWebAggregator;

            yield return ApiResources.CashierWebAggregator;
        }

        public static IEnumerable<Client> GetClients(IdentityAppSettings appSettings, IHostEnvironment environment)
        {
            yield return new Client
            {
                ClientId = "web-spa",
                ClientName = "Web Spa",
                AllowedCorsOrigins = new HashSet<string>
                {
                    appSettings.Client.Web.Endpoints.SpaUrl
                },
                PostLogoutRedirectUris = new HashSet<string>
                {
                    $"{appSettings.Client.Web.Endpoints.SpaUrl}/authentication/logout-callback"
                },
                RedirectUris = new HashSet<string>
                {
                    $"{appSettings.Client.Web.Endpoints.SpaUrl}/authentication/login-callback"
                },
                RequireConsent = false,
                AccessTokenType = AccessTokenType.Reference,
                AllowAccessTokensViaBrowser = true,
                AccessTokenLifetime = 3600,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowedScopes = new HashSet<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    Scopes.Stripe.Name,
                    Scopes.Country.Name,
                    Scopes.Roles.Name,
                    Scopes.Permissions.Name,
                    Scopes.Games.Name,
                    Scopes.IdentityApi.Name,
                    Scopes.PaymentApi.Name,
                    Scopes.CashierApi.Name,
                    Scopes.ChallengesApi.Name,
                    Scopes.GamesApi.Name,
                    Scopes.ClansApi.Name,
                    Scopes.ChallengesWebAggregator.Name,
                    Scopes.CashierWebAggregator.Name
                }
            };

            if (environment.IsDevelopment())
            {
                yield return ApiResources.CashierApi.GetSwaggerClient(appSettings.Service.Endpoints.CashierUrl);

                yield return ApiResources.ChallengesApi.GetSwaggerClient(appSettings.Service.Endpoints.ChallengesUrl);

                yield return ApiResources.ClansApi.GetSwaggerClient(appSettings.Service.Endpoints.ClansUrl);

                yield return ApiResources.GamesApi.GetSwaggerClient(appSettings.Service.Endpoints.GamesUrl);

                yield return ApiResources.IdentityApi.GetSwaggerClient(appSettings.Service.Endpoints.IdentityUrl);

                yield return ApiResources.NotificationsApi.GetSwaggerClient(appSettings.Service.Endpoints.NotificationsUrl);

                yield return ApiResources.PaymentApi.GetSwaggerClient(appSettings.Service.Endpoints.PaymentUrl);

                yield return ApiResources.CashierWebAggregator.GetSwaggerClient(
                    appSettings.Aggregator.Web.Endpoints.CashierUrl,
                    Scopes.CashierApi,
                    Scopes.PaymentApi);

                yield return ApiResources.ChallengesWebAggregator.GetSwaggerClient(
                    appSettings.Aggregator.Web.Endpoints.ChallengesUrl,
                    Scopes.CashierApi,
                    Scopes.GamesApi,
                    Scopes.ChallengesApi);
            }
        }
    }
}
