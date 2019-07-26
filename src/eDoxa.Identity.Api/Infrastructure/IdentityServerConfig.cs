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

using eDoxa.Seedwork.Security.Constants;
using eDoxa.Seedwork.Security.Extensions;
using eDoxa.Seedwork.Security.IdentityServer.Resources;

using IdentityServer4;
using IdentityServer4.Models;

namespace eDoxa.Identity.Api.Infrastructure
{
    public static class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            yield return new IdentityResources.OpenId();

            yield return new IdentityResources.Profile();

            yield return CustomIdentityResources.Roles;

            yield return CustomIdentityResources.Permissions;

            yield return CustomIdentityResources.Stripe;

            yield return CustomIdentityResources.Games;
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            yield return CustomApiResources.IdentityApi;

            yield return CustomApiResources.CashierApi;

            yield return CustomApiResources.ArenaChallengesApi;
        }

        public static IEnumerable<Client> GetClients(IdentityAppSettings appSettings)
        {
            yield return CustomApiResources.IdentityApi.SwaggerClient(appSettings.IdentityServer.IdentityUrl);

            yield return CustomApiResources.CashierApi.SwaggerClient(appSettings.IdentityServer.CashierUrl);

            yield return CustomApiResources.ArenaChallengesApi.SwaggerClient(appSettings.IdentityServer.ArenaChallengesUrl);

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
                    CustomScopes.Roles,
                    CustomScopes.Permissions,
                    CustomScopes.Stripe,
                    CustomScopes.Games,
                    CustomScopes.IdentityApi,
                    CustomScopes.CashierApi,
                    CustomScopes.ArenaChallengesApi
                },
                
            };
        }
    }
}
