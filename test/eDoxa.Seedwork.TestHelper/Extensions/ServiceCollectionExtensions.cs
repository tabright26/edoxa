// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-08-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Security.Claims;

using eDoxa.Seedwork.TestHelper.Fakes;

using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.TestHelper.Extensions
{
    public static class ServiceCollectionExtensions
    {
        internal static void AddFakeAuthentication(this IServiceCollection services, Action<FakeAuthenticationOptions> configureOptions)
        {
            services.AddAuthentication(
                    options =>
                    {
                        options.DefaultAuthenticateScheme = nameof(FakeAuthenticationHandler);
                        options.DefaultChallengeScheme = nameof(FakeAuthenticationHandler);
                    }
                )
                .AddScheme<FakeAuthenticationOptions, FakeAuthenticationHandler>(nameof(FakeAuthenticationHandler), configureOptions);
        }
    }
}
