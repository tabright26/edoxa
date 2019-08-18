// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-08-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Security.Claims;

using eDoxa.Seedwork.Testing.Fakes;

using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Testing.Extensions
{
    public static class ServiceCollectionExtensions
    {
        internal static void AddFakeAuthentication(this IServiceCollection services, Action<FakeAuthenticationOptions> configureOptions)
        {
            services.AddAuthentication(
                    options =>
                    {
                        options.DefaultAuthenticateScheme = FakeAuthenticationDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = FakeAuthenticationDefaults.AuthenticationScheme;
                    }
                )
                .AddScheme<FakeAuthenticationOptions, FakeAuthenticationHandler>(FakeAuthenticationDefaults.AuthenticationScheme, configureOptions);
        }

        // TODO: Must be remove in .Net Core 3.0 (Fixed in AspNetCore.Identity)
        internal static void AddFakeAuthenticationFilter(this IServiceCollection services, params Claim[] claims)
        {
            services.AddMvc(
                options =>
                {
                    options.Filters.Add(new AllowAnonymousFilter());
                    options.Filters.Add(new FakeClaimsPrincipalFilter(claims));
                }
            );
        }
    }
}
