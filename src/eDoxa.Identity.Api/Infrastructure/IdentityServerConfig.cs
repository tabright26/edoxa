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
                    Scopes.Games,
                    Scopes.IdentityApi,
                    Scopes.PaymentApi,
                    Scopes.CashierApi,
                    Scopes.ArenaChallengesApi,
                    Scopes.ArenaGamesLeagueOfLegendsApi,
                    Scopes.OrganizationsClansApi
                }
            };
        }
    }
}
