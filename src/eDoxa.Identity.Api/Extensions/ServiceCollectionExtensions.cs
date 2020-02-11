// Filename: ServiceCollectionExtensions.cs
// Date Created: 2020-02-01
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Linq;

using eDoxa.Identity.Api.Application.Services;
using eDoxa.Identity.Api.Infrastructure;
using eDoxa.Identity.Api.Services;
using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.Infrastructure;
using eDoxa.Seedwork.Application;

using IdentityModel;

using IdentityServer4.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eDoxa.Identity.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(
                    options =>
                    {
                        options.Password.RequireDigit = true;
                        options.Password.RequiredLength = 8;
                        options.Password.RequiredUniqueChars = 1;
                        options.Password.RequireLowercase = true;
                        options.Password.RequireNonAlphanumeric = true;
                        options.Password.RequireUppercase = true;
                        options.Lockout.AllowedForNewUsers = false;
                        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                        options.Lockout.MaxFailedAccessAttempts = 5;
                        options.User.RequireUniqueEmail = true;
                        options.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Subject;
                        options.ClaimsIdentity.UserNameClaimType = CustomClaimTypes.Doxatag;
                        options.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;
                        options.ClaimsIdentity.SecurityStampClaimType = CustomClaimTypes.SecurityStamp;
                        options.SignIn.RequireConfirmedPhoneNumber = false;
                        options.SignIn.RequireConfirmedEmail = false;
                    })
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserRepository>()
                .AddClaimsPrincipalFactory<CustomUserClaimsPrincipalFactory>()
                .AddUserManager<UserService>()
                .AddSignInManager<SignInService>()
                .AddRoleManager<RoleService>()
                .AddUserValidator<CustomUserValidator>();

            services.AddScoped<UserRepository>();
            services.AddScoped<IUserValidator<User>, CustomUserValidator>();
            services.AddScoped<CustomUserClaimsPrincipalFactory>();
            services.AddScoped<UserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<SignInService>();
            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<RoleService>();
            services.AddScoped<IRoleService, RoleService>();
        }

        public static void AddCustomIdentityServer(this IServiceCollection services, IHostEnvironment environment, IdentityAppSettings appSettings)
        {
            services.AddIdentityServer(
                    options =>
                    {
                        options.IssuerUri = appSettings.Endpoints.IdentityUrl;
                        options.Authentication.CookieLifetime = TimeSpan.FromHours(2);
                        options.Events.RaiseInformationEvents = true;
                        options.Events.RaiseSuccessEvents = true;
                        options.Events.RaiseFailureEvents = true;
                        options.Events.RaiseErrorEvents = true;
                        options.UserInteraction.LoginUrl = $"{appSettings.WebSpaUrl}/account/login";
                        options.UserInteraction.LoginReturnUrlParameter = "returnUrl";
                        options.UserInteraction.LogoutUrl = $"{appSettings.WebSpaUrl}/account/logout";
                    })
                .AddApiAuthorization<User, IdentityDbContext>(
                    options =>
                    {
                        options.IdentityResources.Clear();
                        options.IdentityResources.AddRange(IdentityServerConfig.GetIdentityResources().ToArray());

                        options.ApiResources.Clear();
                        options.ApiResources.AddRange(IdentityServerConfig.GetApiResources().ToArray());

                        options.Clients.Clear();
                        options.Clients.AddRange(IdentityServerConfig.GetClients(appSettings, environment).ToArray());
                    })
                .AddProfileService<CustomProfileService>()
                .AddCorsPolicyService<CustomCorsPolicyService>();

            services.AddTransient<IProfileService, CustomProfileService>();
            services.AddTransient<IReturnUrlParser, CustomReturnUrlParser>();
            services.AddSingleton<ICorsPolicyService, CustomCorsPolicyService>();
        }
    }
}
