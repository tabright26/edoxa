// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Identity.Api.Application.Factories;
using eDoxa.Identity.Api.Application.Managers;
using eDoxa.Identity.Api.Application.Services;
using eDoxa.Identity.Api.Application.TokenProviders;
using eDoxa.Identity.Api.Application.TokenProviders.Extenisons;
using eDoxa.Identity.Api.Application.Validators;
using eDoxa.Identity.Api.Infrastructure;
using eDoxa.Identity.Api.Models;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Security.Constants;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Identity.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var healthChecks = services.AddHealthChecks();
            healthChecks.AddAzureKeyVault(configuration);
            healthChecks.AddSqlServer(configuration);
            healthChecks.AddRedis(configuration);
        }

        public static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(
                options =>
                {
                    options.ApiVersionReader = new HeaderApiVersionReader(CustomHeaderNames.Version);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.ReportApiVersions = true;
                }
            );

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VV");
        }

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
            services.ConfigureTokenProviders();
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
    }
}
