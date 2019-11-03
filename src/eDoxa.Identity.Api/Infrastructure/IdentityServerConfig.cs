// Filename: IdentityServerConfig.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Security;
using eDoxa.Swagger.Client.Extensions;

using IdentityServer4;
using IdentityServer4.Models;

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

            yield return Seedwork.Security.IdentityResources.Country;

            yield return Seedwork.Security.IdentityResources.Roles;

            yield return Seedwork.Security.IdentityResources.Permissions;

            yield return Seedwork.Security.IdentityResources.Games;
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
        }

        public static IEnumerable<Client> GetClients(IdentityAppSettings appSettings)
        {
            if (appSettings.Swagger.Enabled)
            {
                yield return ApiResources.IdentityApi.GetSwaggerClient(appSettings.Swagger.Endpoints.IdentityUrl);

                yield return ApiResources.PaymentApi.GetSwaggerClient(appSettings.Swagger.Endpoints.PaymentUrl);

                yield return ApiResources.CashierApi.GetSwaggerClient(appSettings.Swagger.Endpoints.CashierUrl, Scopes.PaymentApi);

                yield return ApiResources.NotificationsApi.GetSwaggerClient(appSettings.Swagger.Endpoints.NotificationsUrl);

                yield return ApiResources.ChallengesApi.GetSwaggerClient(appSettings.Swagger.Endpoints.ChallengesUrl, Scopes.CashierApi, Scopes.GamesApi);

                yield return ApiResources.GamesApi.GetSwaggerClient(appSettings.Swagger.Endpoints.GamesUrl);

                yield return ApiResources.ClansApi.GetSwaggerClient(appSettings.Swagger.Endpoints.ClansUrl); 
            }

            yield return new Client
            {
                ClientId = "edoxa.web.spa",
                ClientName = "eDoxa Web Spa",
                AllowedCorsOrigins = new HashSet<string>
                {
                    appSettings.WebSpaProxyUrl,
                    "http://localhost:5300",
                    "http://127.0.0.1:5300"
                },
                PostLogoutRedirectUris = new HashSet<string>
                {
                    appSettings.WebSpaProxyUrl,
                    "http://localhost:5300",
                    "http://127.0.0.1:5300"
                },
                RedirectUris = new HashSet<string>
                {
                    $"{appSettings.WebSpaProxyUrl}/callback",
                    "http://localhost:5300/callback",
                    "http://127.0.0.1:5300/callback",
                    $"{appSettings.WebSpaProxyUrl}/silent_renew.html",
                    "http://localhost:5300/silent_renew.html",
                    "http://127.0.0.1:5300/silent_renew.html"
                },
                AccessTokenType = AccessTokenType.Reference,
                RequireConsent = false,
                AllowAccessTokensViaBrowser = true,
                UpdateAccessTokenClaimsOnRefresh = true,
                AccessTokenLifetime = 3600,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowedScopes = new HashSet<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    Scopes.Country.Name,
                    Scopes.Roles.Name,
                    Scopes.Permissions.Name,
                    Scopes.Games.Name,
                    Scopes.IdentityApi.Name,
                    Scopes.PaymentApi.Name,
                    Scopes.CashierApi.Name,
                    Scopes.ChallengesApi.Name,
                    Scopes.GamesApi.Name,
                    Scopes.ClansApi.Name
                }
            };
        }
    }
}
