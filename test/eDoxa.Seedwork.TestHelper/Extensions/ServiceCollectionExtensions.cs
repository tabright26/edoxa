// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.TestHelper.Fakes;

using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.TestHelper.Extensions
{
    public static class ServiceCollectionExtensions
    {
        internal static void AddFakeAuthentication(this IServiceCollection services, Action<TestAuthenticationOptions> configureOptions)
        {
            var authenticationOptions = new TestAuthenticationOptions();

            configureOptions(authenticationOptions);

            services.AddAuthentication(
                    options =>
                    {
                        options.DefaultAuthenticateScheme = authenticationOptions.AuthenticationScheme;
                        options.DefaultChallengeScheme = authenticationOptions.AuthenticationScheme;
                    })
                .AddScheme<TestAuthenticationOptions, TestAuthenticationHandler>(authenticationOptions.AuthenticationScheme, configureOptions);
        }
    }
}
