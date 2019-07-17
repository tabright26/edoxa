// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Security.Constants;
using eDoxa.Seedwork.Security.Models;

using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StackExchange.Redis;

namespace eDoxa.Seedwork.Security.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomIdentity(this IServiceCollection services, IHostingEnvironment environment)
        {
            services.AddIdentity<UserModel, RoleModel>(
                    options =>
                    {
                        // Password settings
                        options.Password.RequireDigit = true;
                        options.Password.RequiredLength = 8;
                        options.Password.RequiredUniqueChars = 1;
                        options.Password.RequireLowercase = true;
                        options.Password.RequireNonAlphanumeric = true;
                        options.Password.RequireUppercase = true;

                        // Lockout settings
                        options.Lockout.AllowedForNewUsers = true;
                        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                        options.Lockout.MaxFailedAccessAttempts = 5;

                        // User settings
                        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_#";
                        options.User.RequireUniqueEmail = true;

                        // Claims settings
                        options.ClaimsIdentity.SecurityStampClaimType = CustomClaimTypes.SecurityStamp;

                        // SignIn settings
                        options.SignIn.RequireConfirmedPhoneNumber = false;
                        options.SignIn.RequireConfirmedEmail = environment.IsProduction();

                        // Tokens settings
                        options.Tokens.AuthenticatorTokenProvider = CustomTokenProviders.Authenticator;
                        options.Tokens.ChangeEmailTokenProvider = CustomTokenProviders.ChangeEmail;
                        options.Tokens.ChangePhoneNumberTokenProvider = CustomTokenProviders.ChangePhoneNumber;
                        options.Tokens.EmailConfirmationTokenProvider = CustomTokenProviders.EmailConfirmation;
                        options.Tokens.PasswordResetTokenProvider = CustomTokenProviders.PasswordReset;
                    }
                )
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddClaimsPrincipalFactory<CustomUserClaimsPrincipalFactory>()
                .AddSignInManager<CustomSignInManager>()
                .AddRoleManager<CustomRoleManager>()
                .AddUserManager<CustomUserManager>()
                .AddUserValidator<CustomUserValidator>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<CustomAuthenticatorTokenProvider>(CustomTokenProviders.Authenticator)
                .AddTokenProvider<CustomChangeEmailTokenProvider>(CustomTokenProviders.ChangeEmail)
                .AddTokenProvider<CustomChangePhoneNumberTokenProvider>(CustomTokenProviders.ChangePhoneNumber)
                .AddTokenProvider<CustomEmailConfirmationTokenProvider>(CustomTokenProviders.EmailConfirmation)
                .AddTokenProvider<CustomPasswordResetTokenProvider>(CustomTokenProviders.PasswordReset)
                .AddDefaultUI(UIFramework.Bootstrap4);

            services.AddScoped<CustomUserManager>();
            services.AddScoped<CustomSignInManager>();
            services.AddScoped<CustomRoleManager>();
            services.Configure<CustomAuthenticatorTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(1));
            services.Configure<CustomChangeEmailTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromDays(1));
            services.Configure<CustomChangePhoneNumberTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromDays(1));
            services.Configure<CustomEmailConfirmationTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromDays(2));
            services.Configure<CustomPasswordResetTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(2));
        }

        public static void AddCustomIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityServer(
                    options =>
                    {
                        options.IssuerUri = configuration.GetValue<string>("IdentityServer:Url");

                        options.Authentication.CookieLifetime = TimeSpan.FromHours(2);

                        options.Events.RaiseErrorEvents = true;
                        options.Events.RaiseInformationEvents = true;
                        options.Events.RaiseFailureEvents = true;
                        options.Events.RaiseSuccessEvents = true;

                        options.UserInteraction.LoginUrl = "/Account/Login";
                        options.UserInteraction.LogoutUrl = "/Account/Logout";
                    }
                )
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients(configuration))
                .AddProfileService<CustomProfileService<UserModel>>()
                .AddAspNetIdentity<UserModel>()
                .AddCorsPolicyService<CustomCorsPolicyService>();
        }

        public static void AddCookiePolicy(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                }
            );
        }

        public static void AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(
                options =>
                {
                    options.AddPolicy(
                        CustomPolicies.CorsPolicy,
                        builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(_ => true)
                    );
                }
            );
        }

        // TODO: THIS IS NOT TESTED.
        public static void AddDataProtection(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("AzureKubernetesService:Enable"))
            {
                services.AddDataProtection(
                        options =>
                        {
                            options.ApplicationDiscriminator = configuration["ApplicationDiscriminator"];
                        }
                    )
                    .PersistKeysToRedis(ConnectionMultiplexer.Connect(configuration.GetConnectionString(CustomConnectionStrings.Redis)), "data-protection");
            }
        }

        public static void AddAuthentication(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostingEnvironment environment,
            IDictionary<string, ApiResource> apiResources
        )
        {
            var builder = services.AddAuthentication();

            foreach (var apiResource in apiResources)
            {
                builder.AddIdentityServerAuthentication(
                    apiResource.Key,
                    options =>
                    {
                        options.Authority = configuration.GetValue<string>("IdentityServer:Url");
                        options.ApiName = apiResource.Value.Name;
                        options.ApiSecret = "secret";
                        options.RequireHttpsMetadata = environment.IsProduction();
                    }
                );
            }
        }

        public static void AddAuthentication(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostingEnvironment environment,
            ApiResource apiResource
        )
        {
            var authority = configuration.GetValue<string>("IdentityServer:Url");

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(
                    options =>
                    {
                        options.ApiName = apiResource.Name;
                        options.Authority = authority;
                        options.RequireHttpsMetadata = environment.IsProduction();
                        options.ApiSecret = "secret";
                    }
                );
        }
    }
}
