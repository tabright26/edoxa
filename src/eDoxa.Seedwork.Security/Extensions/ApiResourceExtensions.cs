// Filename: ApiResourceExtensions.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using IdentityServer4.Models;

namespace eDoxa.Seedwork.Security.Extensions
{
    // TODO: Must be refactored. These extension methods are not supposed to be in eDoxa.Seedwork.Security.
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

        public static Client SwaggerClient(this ApiResource apiResource, string redirectUri)
        {
            return new Client
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
                UpdateAccessTokenClaimsOnRefresh = true,
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
        }
    }
}
