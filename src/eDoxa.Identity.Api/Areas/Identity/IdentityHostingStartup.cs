// Filename: IdentityHostingStartup.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Identity.Api.Areas.Identity;
using eDoxa.Identity.Api.Areas.Identity.Extensions;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Areas.Identity.Validators;
using eDoxa.Identity.Api.Infrastructure;
using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Seedwork.Security.Constants;

using IdentityModel;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]

namespace eDoxa.Identity.Api.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure([NotNull] IWebHostBuilder builder)
        {
            builder.ConfigureServices(
                (context, services) =>
                {
                    services.Configure<CookiePolicyOptions>(
                        options =>
                        {
                            options.CheckConsentNeeded = _ => true;
                            options.MinimumSameSitePolicy = SameSiteMode.None;
                        }
                    );

                    services.AddScoped<IUserValidator<User>, EmailValidator>();
                    services.AddScoped<IUserValidator<User>, PhoneNumberValidator>();
                    services.AddScoped<IUserValidator<User>, UserNameValidator>();

                    services.AddIdentity<User, Role>(
                            options =>
                            {
                                options.Password.RequireDigit = true;
                                options.Password.RequiredLength = 8;
                                options.Password.RequiredUniqueChars = 1;
                                options.Password.RequireLowercase = true;
                                options.Password.RequireNonAlphanumeric = true;
                                options.Password.RequireUppercase = true;

                                options.Lockout.AllowedForNewUsers = true;
                                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                                options.Lockout.MaxFailedAccessAttempts = 5;

                                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_#";
                                options.User.RequireUniqueEmail = true;

                                options.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Subject;
                                options.ClaimsIdentity.UserNameClaimType = JwtClaimTypes.Name;
                                options.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;
                                options.ClaimsIdentity.SecurityStampClaimType = CustomClaimTypes.SecurityStamp;

                                options.SignIn.RequireConfirmedPhoneNumber = false;
                                options.SignIn.RequireConfirmedEmail = context.HostingEnvironment.IsProduction();

                                options.Tokens.AuthenticatorTokenProvider = CustomTokenProviders.Authenticator;
                                options.Tokens.ChangeEmailTokenProvider = CustomTokenProviders.ChangeEmail;
                                options.Tokens.ChangePhoneNumberTokenProvider = CustomTokenProviders.ChangePhoneNumber;
                                options.Tokens.EmailConfirmationTokenProvider = CustomTokenProviders.EmailConfirmation;
                                options.Tokens.PasswordResetTokenProvider = CustomTokenProviders.PasswordReset;
                            }
                        )
                        .AddEntityFrameworkStores<IdentityDbContext>()
                        .AddUserStore<CustomUserStore>()
                        .AddTokenProviders(
                            options =>
                            {
                                options.Authenticator.TokenLifespan = TimeSpan.FromHours(1);
                                options.ChangeEmail.TokenLifespan = TimeSpan.FromDays(1);
                                options.ChangePhoneNumber.TokenLifespan = TimeSpan.FromDays(1);
                                options.EmailConfirmation.TokenLifespan = TimeSpan.FromDays(2);
                                options.PasswordReset.TokenLifespan = TimeSpan.FromHours(2);
                            }
                        )
                        .AddClaimsPrincipalFactory<CustomUserClaimsPrincipalFactory>()
                        .AddUserManager<CustomUserManager>()
                        .AddUserValidator<EmailValidator>()
                        .AddUserValidator<PhoneNumberValidator>()
                        .AddUserValidator<UserNameValidator>()
                        .AddSignInManager<CustomSignInManager>()
                        .AddRoleManager<CustomRoleManager>()
                        .BuildCustomServices();

                    services.Configure<PasswordHasherOptions>(
                        option =>
                        {
                            option.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3;
                            option.IterationCount = context.HostingEnvironment.IsProduction() ? 100000 : 1;
                        }
                    );
                }
            );
        }
    }
}
