// Filename: IdentityBuilderExtensions.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Identity.Api.Services;
using eDoxa.Identity.Api.TokenProviders;
using eDoxa.Identity.Api.TokenProviders.Options;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using static eDoxa.Identity.Api.Constants.TokenProviders;

namespace eDoxa.Identity.Api.Areas.Identity.Extensions
{
    public static class IdentityBuilderExtensions
    {
        public static void BuildCustomServices(this IdentityBuilder builder)
        {
            var services = builder.Services;
            services.AddScoped<Services.UserStore>();
            services.AddScoped<CustomUserClaimsPrincipalFactory>();
            services.AddScoped<CustomIdentityErrorDescriber>();
            services.AddScoped<UserManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<SignInManager>();
            services.AddScoped<ISignInManager, SignInManager>();
            services.AddScoped<RoleManager>();
            services.AddScoped<IRoleManager, RoleManager>();
        }

        public static IdentityBuilder AddTokenProviders(this IdentityBuilder builder, Action<TokenProviderOptions> options)
        {
            return builder.AddDefaultTokenProviders()
                .AddTokenProvider<AuthenticatorTokenProvider>(Authenticator)
                .AddTokenProvider<ChangeEmailTokenProvider>(ChangeEmail)
                .AddTokenProvider<ChangePhoneNumberTokenProvider>(ChangePhoneNumber)
                .AddTokenProvider<EmailConfirmationTokenProvider>(EmailConfirmation)
                .AddTokenProvider<PasswordResetTokenProvider>(PasswordReset)
                .AddTokenProviderOptions(options);
        }

        private static IdentityBuilder AddTokenProviderOptions(this IdentityBuilder builder, Action<TokenProviderOptions> options)
        {
            var services = builder.Services;
            var tokenProviderOptions = new TokenProviderOptions();
            options(tokenProviderOptions);
            services.Configure<AuthenticatorTokenProviderOptions>(x => x.TokenLifespan = tokenProviderOptions.Authenticator.TokenLifespan);
            services.Configure<ChangeEmailTokenProviderOptions>(x => x.TokenLifespan = tokenProviderOptions.ChangeEmail.TokenLifespan);
            services.Configure<ChangePhoneNumberTokenProviderOptions>(x => x.TokenLifespan = tokenProviderOptions.ChangePhoneNumber.TokenLifespan);
            services.Configure<EmailConfirmationTokenProviderOptions>(x => x.TokenLifespan = tokenProviderOptions.EmailConfirmation.TokenLifespan);
            services.Configure<PasswordResetTokenProviderOptions>(x => x.TokenLifespan = tokenProviderOptions.PasswordReset.TokenLifespan);
            return builder;
        }
    }
}
