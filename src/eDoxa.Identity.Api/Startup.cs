// Filename: Startup.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IO;
using System.Reflection;

using Autofac;

using AutoMapper;

using eDoxa.Identity.Api.Areas.Identity.Extensions;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Areas.Identity.Validators;
using eDoxa.Identity.Api.Extensions;
using eDoxa.Identity.Api.Infrastructure;
using eDoxa.Identity.Api.Infrastructure.Data;
using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Identity.Api.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.Modules;
using eDoxa.Seedwork.Application.Swagger.Extensions;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Security.Constants;
using eDoxa.Seedwork.Security.Extensions;
using eDoxa.Seedwork.Security.Hosting.Extensions;
using eDoxa.Seedwork.Security.Middlewares;
using eDoxa.ServiceBus.Modules;

using FluentValidation.AspNetCore;

using IdentityModel;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using static eDoxa.Seedwork.Security.IdentityServer.Resources.CustomApiResources;

namespace eDoxa.Identity.Api
{
    public sealed class Startup
    {
        private static readonly string XmlCommentsFilePath = Path.Combine(
            AppContext.BaseDirectory,
            $"{typeof(Startup).GetTypeInfo().Assembly.GetName().Name}.xml"
        );

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            AppSettings = configuration.GetAppSettings<IdentityAppSettings>(IdentityApi);
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        private IdentityAppSettings AppSettings { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppSettings<IdentityAppSettings>(Configuration);

            services.AddHealthChecks(AppSettings);

            services.AddDataProtection(Configuration);

            services.AddDbContext<IdentityDbContext, IdentityDbContextData>(AppSettings.ConnectionStrings.SqlServer, Assembly.GetAssembly(typeof(Startup)));

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
                        options.SignIn.RequireConfirmedEmail = HostingEnvironment.IsProduction();

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
                    option.IterationCount = HostingEnvironment.IsProduction() ? 100000 : 1;
                }
            );

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddControllersAsServices()
                .AddRazorPagesOptions(
                    options =>
                    {
                        options.AllowAreas = true;
                        options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                        options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                    }
                )
                .AddFluentValidation(config => config.RunDefaultMvcValidationAfterFluentValidationExecutes = false);

            services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.ApiVersionReader = new HeaderApiVersionReader(CustomHeaderNames.Version);
                }
            );

            services.AddAutoMapper(Assembly.GetAssembly(typeof(Startup)), Assembly.GetAssembly(typeof(IdentityDbContext)));

            services.AddIdentityServer(
                    options =>
                    {
                        options.IssuerUri = AppSettings.Authority.PrivateUrl;
                        options.Authentication.CookieLifetime = TimeSpan.FromHours(2);
                        options.Events.RaiseInformationEvents = true;
                        options.Events.RaiseSuccessEvents = true;
                        options.Events.RaiseFailureEvents = true;
                        options.Events.RaiseErrorEvents = true;
                        options.UserInteraction.LoginUrl = "/Account/Login";
                        options.UserInteraction.LoginReturnUrlParameter = "returnUrl";
                        options.UserInteraction.LogoutUrl = "/Account/Logout";
                    }
                )
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                .AddInMemoryClients(IdentityServerConfig.GetClients(AppSettings))
                .AddCorsPolicyService<CustomCorsPolicyService>()
                .AddProfileService<CustomProfileService>()
                .AddAspNetIdentity<User>()
                .BuildCustomServices();

            services.AddAuthentication(HostingEnvironment, AppSettings);
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            this.ConfigureServices(services);

            services.AddSwagger(XmlCommentsFilePath, AppSettings);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<DomainEventModule>();

            builder.RegisterModule(new ServiceBusModule<Startup>(AppSettings));

            builder.RegisterModule<IdentityApiModule>();
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseHealthChecks();

            if (HostingEnvironment.IsDevelopment())
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

            if (HostingEnvironment.IsTesting())
            {
                application.UseMiddleware<TestAuthenticationMiddleware>();
            }
            else
            {
                application.UseIdentityServer();
            }

            application.UseMvcWithDefaultRoute();

            application.UseServiceBusSubscriber();
        }

        public void ConfigureDevelopment(IApplicationBuilder application, IApiVersionDescriptionProvider provider)
        {
            this.Configure(application);

            application.UseSwagger(provider, AppSettings);
        }
    }
}
