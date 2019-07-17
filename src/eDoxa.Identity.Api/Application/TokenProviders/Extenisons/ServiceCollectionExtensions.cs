// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Identity.Api.Application.TokenProviders.Options;

using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Identity.Api.Application.TokenProviders.Extenisons
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureTokenProviders(this IServiceCollection services)
        {
            services.Configure<CustomAuthenticatorTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(1));
            services.Configure<CustomChangeEmailTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromDays(1));
            services.Configure<CustomChangePhoneNumberTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromDays(1));
            services.Configure<CustomEmailConfirmationTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromDays(2));
            services.Configure<CustomPasswordResetTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(2));
        }
    }
}
