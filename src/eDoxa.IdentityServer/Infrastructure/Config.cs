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
using eDoxa.Seedwork.Security.IdentityServer.Extensions;
using eDoxa.Seedwork.Security.IdentityServer.Resources;

using IdentityServer4;
using IdentityServer4.Models;

using Microsoft.Extensions.Configuration;

namespace eDoxa.IdentityServer.Infrastructure
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            yield return new IdentityResources.OpenId();

            yield return new IdentityResources.Profile();

            yield return CustomIdentityResources.Roles;

            yield return CustomIdentityResources.Permissions;
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            yield return CustomApiResources.Identity;

            yield return CustomApiResources.Cashier;

            yield return CustomApiResources.ArenaChallenges;
        }

        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            yield return CustomApiResources.Identity.SwaggerClient(configuration["Identity:Url"]);

            yield return CustomApiResources.Cashier.SwaggerClient(configuration["Cashier:Url"]);

            yield return CustomApiResources.ArenaChallenges.SwaggerClient(configuration["Challenge:Url"]);

            yield return new Client
            {
                ClientId = "edoxa.web.spa",
                ClientName = "eDoxa Web Spa",
                AllowedCorsOrigins = new HashSet<string>
                {
                    configuration["Web:Spa:Url"],
                    "http://localhost:5300",
                    "http://127.0.0.1:5300"
                },
                PostLogoutRedirectUris = new HashSet<string>
                {
                    configuration["Web:Spa:Url"],
                    "http://localhost:5300",
                    "http://127.0.0.1:5300"
                },
                RedirectUris = new HashSet<string>
                {
                    $"{configuration["Web:Spa:Url"]}/callback",
                    "http://localhost:5300/callback",
                    "http://127.0.0.1:5300/callback"
                },
                AccessTokenType = AccessTokenType.Reference,
                RequireConsent = false,
                AllowAccessTokensViaBrowser = true,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowedScopes = new HashSet<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    CustomScopes.Roles,
                    CustomScopes.Permissions,
                    CustomScopes.IdentityApi,
                    CustomScopes.CashierApi,
                    CustomScopes.ChallengeApi
                }
            };
        }
    }
}
