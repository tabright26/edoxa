// Filename: Config.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Security;

using IdentityServer4;
using IdentityServer4.Models;

using Microsoft.Extensions.Configuration;

namespace eDoxa.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            yield return new IdentityResources.OpenId();

            yield return new IdentityResources.Profile();

            yield return new IdentityResources.Email();

            yield return new IdentityResources.Phone();

            yield return new CustomIdentityResources.Role();

            yield return new CustomIdentityResources.Permission();
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            yield return new CustomApiResources.IdentityApi();

            yield return new CustomApiResources.CashierApi();

            yield return new CustomApiResources.ChallengeApi();
        }

        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            yield return new Client
            {
                AllowAccessTokensViaBrowser = true,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowedScopes = new HashSet<string>
                {
                    CustomScopes.IdentityApi
                },
                ClientId = "edoxa.identity.swagger.client",
                ClientName = "eDoxa Identity API (Swagger UI)",
                RequireConsent = false,
                RedirectUris = new HashSet<string>
                {
                    $"{configuration["Identity:Url"]}/oauth2-redirect.html"
                },
                PostLogoutRedirectUris = new HashSet<string>
                {
                    $"{configuration["Identity:Url"]}/"
                }
            };

            yield return new Client
            {
                AllowAccessTokensViaBrowser = true,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowedScopes = new HashSet<string>
                {
                    CustomScopes.ChallengeApi
                },
                ClientId = "edoxa.challenge.swagger.client",
                ClientName = "eDoxa Challenge API (Swagger UI)",
                RequireConsent = false,
                RedirectUris = new HashSet<string>
                {
                    $"{configuration["Challenge:Url"]}/oauth2-redirect.html"
                },
                PostLogoutRedirectUris = new HashSet<string>
                {
                    $"{configuration["Challenge:Url"]}/"
                }
            };

            yield return new Client
            {
                AllowAccessTokensViaBrowser = true,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowedScopes = new HashSet<string>
                {
                    CustomScopes.CashierApi
                },
                ClientId = "edoxa.cashier.swagger.client",
                ClientName = "eDoxa Cashier API (Swagger UI)",
                RequireConsent = false,
                RedirectUris = new HashSet<string>
                {
                    $"{configuration["Cashier:Url"]}/oauth2-redirect.html"
                },
                PostLogoutRedirectUris = new HashSet<string>
                {
                    $"{configuration["Cashier:Url"]}/"
                }
            };

            yield return new Client
            {
                ClientId = "edoxa.web.spa",
                ClientName = "eDoxa Web Spa",
                AllowedCorsOrigins = new HashSet<string>
                {
                    configuration["WebSpa:Url"],
                    "http://localhost:5300",
                    "http://127.0.0.1:5300"
                },
                PostLogoutRedirectUris = new HashSet<string>
                {
                    configuration["WebSpa:Url"],
                    "http://localhost:5300",
                    "http://127.0.0.1:5300"
                },
                RedirectUris = new HashSet<string>
                {
                    $"{configuration["WebSpa:Url"]}/callback",
                    "http://localhost:5300/callback",
                    "http://127.0.0.1:5300/callback"
                },
                RequireConsent = false,
                AllowAccessTokensViaBrowser = true,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowedScopes = new HashSet<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Phone,
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