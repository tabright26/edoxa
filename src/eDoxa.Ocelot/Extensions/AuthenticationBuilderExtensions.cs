// Filename: AuthenticationBuilderExtensions.cs
// Date Created: 2019-06-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using IdentityServer4.AccessTokenValidation;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;

namespace eDoxa.Ocelot.Extensions
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddIdentityApiAuthentication(this AuthenticationBuilder builder, string authenticationProviderKey, string authority)
        {
            builder.AddIdentityServerAuthentication(
                authenticationProviderKey,
                options =>
                {
                    options.Authority = authority;
                    options.ApiName = "edoxa.identity.api";
                    options.SupportedTokens = SupportedTokens.Both;
                    options.ApiSecret = "secret";
                    options.RequireHttpsMetadata = false;
                }
            );

            return builder;
        }

        public static AuthenticationBuilder AddCashierApiAuthentication(this AuthenticationBuilder builder, string authenticationProviderKey, string authority)
        {
            builder.AddIdentityServerAuthentication(
                authenticationProviderKey,
                options =>
                {
                    options.Authority = authority;
                    options.ApiName = "edoxa.cashier.api";
                    options.SupportedTokens = SupportedTokens.Both;
                    options.ApiSecret = "secret";
                    options.RequireHttpsMetadata = false;
                }
            );

            return builder;
        }

        public static AuthenticationBuilder AddArenaChallengesApiAuthentication(
            this AuthenticationBuilder builder,
            string authenticationProviderKey,
            string authority
        )
        {
            builder.AddIdentityServerAuthentication(
                authenticationProviderKey,
                options =>
                {
                    options.Authority = authority;
                    options.ApiName = "edoxa.challenge.api";
                    options.SupportedTokens = SupportedTokens.Both;
                    options.ApiSecret = "secret";
                    options.RequireHttpsMetadata = false;
                }
            );

            return builder;
        }
    }
}
