// Filename: ConfigurationDbContextSeeder.cs
// Date Created: 2019-04-13
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.IdentityServer;

using IdentityServer4;
using IdentityServer4.Models;

using Microsoft.Extensions.Configuration;

namespace eDoxa.Identity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources()
        {
            yield return new IdentityResources.OpenId();

            yield return new IdentityResources.Profile();

            yield return new IdentityResources.Email();

            yield return new IdentityResources.Phone();
        }

        public static IEnumerable<ApiResource> ApiResources()
        {
            yield return new CustomApiResources.IdentityApi();

            yield return new CustomApiResources.CashierApi();

            yield return new CustomApiResources.ChallengesApi();

            yield return new CustomApiResources.NotificationsApi();
        }

        public static IEnumerable<Client> Clients(IConfiguration configuration)
        {
            yield return new Client
            {
                AllowAccessTokensViaBrowser = true,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowedScopes =
                {
                    CustomScopes.IdentityApi
                },
                ClientId = "edoxa.identity.swagger.client",
                ClientName = "eDoxa Identity API (Swagger UI)",
                RedirectUris =
                {
                    $"{configuration["Identity:Url"]}/swagger/oauth2-redirect.html"
                },
                PostLogoutRedirectUris =
                {
                    $"{configuration["Identity:Url"]}/swagger/"
                }
            };

            yield return new Client
            {
                AllowAccessTokensViaBrowser = true,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowedScopes =
                {
                    CustomScopes.ChallengesApi
                },
                ClientId = "edoxa.challenges.swagger.client",
                ClientName = "eDoxa Challenges API (Swagger UI)",
                RedirectUris =
                {
                    $"{configuration["Challenges:Url"]}/swagger/oauth2-redirect.html"
                },
                PostLogoutRedirectUris =
                {
                    $"{configuration["Challenges:Url"]}/swagger/"
                }
            };

            yield return new Client
            {
                AllowAccessTokensViaBrowser = true,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowedScopes =
                {
                    CustomScopes.CashierApi
                },
                ClientId = "edoxa.cashier.swagger.client",
                ClientName = "eDoxa Cashier API (Swagger UI)",
                RedirectUris =
                {
                    $"{configuration["Cashier:Url"]}/swagger/oauth2-redirect.html"
                },
                PostLogoutRedirectUris =
                {
                    $"{configuration["Cashier:Url"]}/swagger/"
                }
            };

            yield return new Client
            {
                AllowAccessTokensViaBrowser = true,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowedScopes =
                {
                    CustomScopes.NotificationsApi
                },
                ClientId = "edoxa.notifications.swagger.client",
                ClientName = "eDoxa Notifications API (Swagger UI)",
                RedirectUris =
                {
                    $"{configuration["Notifications:Url"]}/swagger/oauth2-redirect.html"
                },
                PostLogoutRedirectUris =
                {
                    $"{configuration["Notifications:Url"]}/swagger/"
                }
            };

            yield return new Client
            {
                ClientId = "edoxa.web.spa",
                ClientName = "eDoxa Web Spa",
                AllowedCorsOrigins =
                {
                    configuration["WebSpa:Url"]
                },
                PostLogoutRedirectUris =
                {
                    configuration["WebSpa:Url"]
                },
                RedirectUris =
                {
                    $"{configuration["WebSpa:Url"]}/callback"
                },
                RequireConsent = false,
                AllowAccessTokensViaBrowser = true,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Phone,
                    CustomScopes.IdentityApi,
                    CustomScopes.CashierApi,
                    CustomScopes.ChallengesApi,
                    CustomScopes.NotificationsApi
                }
            };
        }
    }
}