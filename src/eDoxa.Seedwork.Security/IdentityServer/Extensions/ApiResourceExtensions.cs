// Filename: ApiResourceExtensions.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using IdentityServer4.Models;

namespace eDoxa.Seedwork.Security.IdentityServer.Extensions
{
    public static class ApiResourceExtensions
    {
        public static string SwaggerClientId(this ApiResource apiResource)
        {
            return apiResource.Name + ".swagger";
        }

        public static string SwaggerClientName(this ApiResource apiResource)
        {
            return apiResource.DisplayName + " (Swagger UI)";
        }

        // TODO: To refactor.
        public static Client SwaggerClient(this ApiResource apiResource, string redirectUri, ApiResource[] allowedScopes = null)
        {
            var client = new Client
            {
                AllowAccessTokensViaBrowser = true,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowedScopes = new HashSet<string>
                {
                    apiResource.Name
                },
                AccessTokenType = AccessTokenType.Reference,
                ClientId = apiResource.SwaggerClientId(),
                ClientName = apiResource.SwaggerClientName(),
                RequireConsent = false,
                RedirectUris = new HashSet<string>
                {
                    $"{redirectUri}/oauth2-redirect.html"
                },
                PostLogoutRedirectUris = new HashSet<string>
                {
                    $"{redirectUri}/"
                }
            };

            if (allowedScopes != null)
            {
                foreach (var allowedScope in allowedScopes)
                {
                    client.AllowedScopes.Add(allowedScope.Name);
                }
            }

            return client;
        }
    }
}
