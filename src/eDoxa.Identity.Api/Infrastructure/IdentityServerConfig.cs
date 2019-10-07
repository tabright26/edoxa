// Filename: Config.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Seedwork.Security;
using eDoxa.Swagger.Client.Extensions;
using IdentityServer4;
using IdentityServer4.Models;

using IdentityResources = eDoxa.Seedwork.Security.IdentityResources;

namespace eDoxa.Identity.Api.Infrastructure
{
    public static class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            yield return new IdentityServer4.Models.IdentityResources.OpenId();

            yield return new IdentityServer4.Models.IdentityResources.Profile();

            yield return new IdentityServer4.Models.IdentityResources.Email();

            yield return new IdentityServer4.Models.IdentityResources.Phone();

            yield return new IdentityServer4.Models.IdentityResources.Address();

            yield return IdentityResources.Roles;

            yield return IdentityResources.Permissions;

            yield return IdentityResources.Stripe;

            yield return IdentityResources.Games;
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            yield return ApiResources.IdentityApi;

            yield return ApiResources.PaymentApi;

            yield return ApiResources.CashierApi;

            yield return ApiResources.NotificationsApi;

            yield return ApiResources.ArenaChallengesApi;

            yield return ApiResources.ArenaGamesLeagueOfLegendsApi;

            yield return ApiResources.OrganizationsClansApi;
        }

        public static IEnumerable<Client> GetClients(IdentityAppSettings appSettings)
        {
            yield return ApiResources.IdentityApi.GetSwaggerClient(appSettings.IdentityServer.IdentityUrl);

            yield return ApiResources.PaymentApi.GetSwaggerClient(appSettings.IdentityServer.PaymentUrl);

            yield return ApiResources.CashierApi.GetSwaggerClient(appSettings.IdentityServer.CashierUrl);

            yield return ApiResources.NotificationsApi.GetSwaggerClient(appSettings.IdentityServer.NotificationsUrl);

            yield return ApiResources.ArenaChallengesApi.GetSwaggerClient(appSettings.IdentityServer.ArenaChallengesUrl);

            yield return ApiResources.ArenaGamesLeagueOfLegendsApi.GetSwaggerClient(appSettings.IdentityServer.ArenaGamesLeagueOfLegendsUrl);

            yield return ApiResources.OrganizationsClansApi.GetSwaggerClient(appSettings.IdentityServer.OrganizationsClansUrl);

            yield return new Client
            {
                ClientId = "edoxa.web.spa",
                ClientName = "eDoxa Web Spa",
                AllowedCorsOrigins = new HashSet<string>
                {
                    appSettings.IdentityServer.Web.SpaUrl,
                    "http://localhost:5300",
                    "http://127.0.0.1:5300"
                },
                PostLogoutRedirectUris = new HashSet<string>
                {
                    appSettings.IdentityServer.Web.SpaUrl,
                    "http://localhost:5300",
                    "http://127.0.0.1:5300"
                },
                RedirectUris = new HashSet<string>
                {
                    $"{appSettings.IdentityServer.Web.SpaUrl}/callback",
                    "http://localhost:5300/callback",
                    "http://127.0.0.1:5300/callback",
                    $"{appSettings.IdentityServer.Web.SpaUrl}/silent_renew.html",
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
                    Scopes.Roles,
                    Scopes.Permissions,
                    Scopes.Stripe,
                    Scopes.Games,
                    Scopes.IdentityApi,
                    Scopes.CashierApi,
                    Scopes.ArenaChallengesApi,
                    Scopes.OrganizationsClans
                },
                
            };
        }
    }
}
