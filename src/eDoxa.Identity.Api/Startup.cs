// Filename: Startup.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Reflection;

using AutoMapper;

using eDoxa.Identity.Api.Areas.Identity.Extensions;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Areas.Identity.Validators;
using eDoxa.Identity.Api.Extensions;
using eDoxa.Identity.Api.Infrastructure;
using eDoxa.Identity.Api.Infrastructure.Data;
using eDoxa.Identity.Api.Models;
using eDoxa.IntegrationEvents.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.Swagger.Extensions;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Security.Constants;
using eDoxa.Seedwork.Security.Extensions;
using eDoxa.Seedwork.Security.Hosting.Extensions;
using eDoxa.Seedwork.Security.IdentityServer.Resources;
using eDoxa.Seedwork.Security.Middlewares;

using FluentValidation.AspNetCore;

using IdentityModel;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        private IHostingEnvironment Environment { get; }

        private IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<IdentityApiOptions>(Configuration);

            services.AddHealthChecks(Configuration);

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = _ => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                }
            );

            services.AddEntityFrameworkSqlServer();

            services.AddIntegrationEventDbContext(Configuration, Assembly.GetAssembly(typeof(Startup)));

            services.AddDbContext<IdentityDbContext, IdentityDbContextData>(Configuration, Assembly.GetAssembly(typeof(Startup)));

            services.AddDataProtection(Configuration);

            //services.AddScoped<IUserValidator<User>, EmailValidator>();
            services.AddScoped<IUserValidator<User>, UserNameValidator>();
            //services.AddScoped<IUserValidator<User>, PhoneNumberValidator>();

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
                        options.SignIn.RequireConfirmedEmail = Environment.IsProduction();

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
                .AddUserValidator<UserNameValidator>()
                .AddSignInManager<CustomSignInManager>()
                .AddRoleManager<CustomRoleManager>()
                .BuildCustomServices();

            //services.ConfigureApplicationCookie(
            //    options =>
            //    {
            //        options.LoginPath = "/Identity/Account/Login";
            //        options.LogoutPath = "/Identity/Account/Logout";
            //        options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            //    }
            //);

            //services.Configure<PasswordHasherOptions>(
            //    option =>
            //    {
            //        option.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3;
            //        option.IterationCount = 100000;
            //    }
            //);

            services.AddVersioning();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMvcFilters();

            //services.AddMvc()
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            //    .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
            //    .AddControllersAsServices()
            //    .AddRazorPagesOptions(
            //        options =>
            //        {
            //            options.AllowAreas = true;
            //            options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
            //            options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
            //        }
            //    ).AddFluentValidation(config => config.RunDefaultMvcValidationAfterFluentValidationExecutes = false);



            services.AddSwagger(Configuration, Environment, CustomApiResources.Identity);

            services.AddCorsPolicy();

            services.AddCustomIdentityServer(Configuration);

            services.AddServiceBus(Configuration);

            services.AddAuthentication(Configuration, Environment, CustomApiResources.Identity);

            return this.BuildModule(services);
        }

        public void Configure(IApplicationBuilder application, IApiVersionDescriptionProvider provider)
        {
            application.UseHealthChecks();

            application.UseCorsPolicy();

            if (Environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }
            else
            {
                application.UseExceptionHandler("/Home/Error");
                application.UseHsts();
            }

            //application.UseCustomExceptionHandler();

            application.UseHttpsRedirection();
            application.UseStaticFiles();
            application.UseForwardedHeaders();
            application.UseCookiePolicy();

            if (Environment.IsTesting())
            {
                application.UseMiddleware<TestAuthenticationMiddleware>();
            }
            else
            {
                application.UseIdentityServer();
            }

            application.UseSwagger(Environment, provider, CustomApiResources.Identity);

            application.UseMvcWithDefaultRoute();

            application.UseIntegrationEventSubscriptions();
        }

        // TODO: Required by integration and functional tests.
        protected virtual IServiceProvider BuildModule(IServiceCollection services)
        {
            return services.Build<IdentityModule>();
        }
    }
}
