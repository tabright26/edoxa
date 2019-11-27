// Filename: Startup.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;

using Autofac;

using AutoMapper;

using eDoxa.Identity.Api.Areas.Identity;
using eDoxa.Identity.Api.Areas.Identity.Constants;
using eDoxa.Identity.Api.Areas.Identity.Extensions;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Extensions;
using eDoxa.Identity.Api.Infrastructure;
using eDoxa.Identity.Api.Infrastructure.Data;
using eDoxa.Identity.Api.IntegrationEvents.Extensions;
using eDoxa.Identity.Api.Services;
using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Application.DevTools.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.Validations;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Monitoring;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Security;
using eDoxa.Seedwork.Security.Extensions;
using eDoxa.ServiceBus.Abstractions;
using eDoxa.ServiceBus.Azure.Modules;

using FluentValidation;
using FluentValidation.AspNetCore;

using HealthChecks.UI.Client;

using IdentityModel;

using IdentityServer4.AccessTokenValidation;

using MediatR;

using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using static eDoxa.Seedwork.Security.ApiResources;

namespace eDoxa.Identity.Api
{
    public sealed class Startup
    {
        private static readonly string XmlCommentsFilePath = Path.Combine(
            AppContext.BaseDirectory,
            $"{typeof(Startup).GetTypeInfo().Assembly.GetName().Name}.xml");

        static Startup()
        {
            TelemetryDebugWriter.IsTracingDisabled = true;
            ValidatorOptions.PropertyNameResolver = CamelCasePropertyNameResolver.ResolvePropertyName;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            AppSettings = configuration.GetAppSettings<IdentityAppSettings>(IdentityApi);
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment HostingEnvironment { get; }

        private IdentityAppSettings AppSettings { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppSettings<IdentityAppSettings>(Configuration);

            services.Configure<AdminOptions>(Configuration.GetSection("Admin"));

            services.AddHealthChecks()
                .AddCheck("liveness", () => HealthCheckResult.Healthy())
                .AddAzureKeyVault(Configuration)
                .AddSqlServer(Configuration)
                .AddRedis(Configuration)
                .AddAzureServiceBusTopic(Configuration);

            services.AddDataProtection(Configuration, AppNames.IdentityApi);

            services.AddDbContext<IdentityDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetSqlServerConnectionString()!,
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(Startup))!.GetName().Name);
                        sqlServerOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                    }));

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = _ => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

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

                        options.User.RequireUniqueEmail = true;

                        options.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Subject;
                        options.ClaimsIdentity.UserNameClaimType = ClaimTypes.Doxatag;
                        options.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;
                        options.ClaimsIdentity.SecurityStampClaimType = ClaimTypes.SecurityStamp;

                        options.SignIn.RequireConfirmedPhoneNumber = false;
                        options.SignIn.RequireConfirmedEmail = false; // TODO: Should be true in prod HostingEnvironment.IsProduction();

                        options.Tokens.AuthenticatorTokenProvider = CustomTokenProviders.Authenticator;
                        options.Tokens.ChangeEmailTokenProvider = CustomTokenProviders.ChangeEmail;
                        options.Tokens.ChangePhoneNumberTokenProvider = CustomTokenProviders.ChangePhoneNumber;
                        options.Tokens.EmailConfirmationTokenProvider = CustomTokenProviders.EmailConfirmation;
                        options.Tokens.PasswordResetTokenProvider = CustomTokenProviders.PasswordReset;
                    })
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddUserStore<UserStore>()
                .AddTokenProviders(
                    options =>
                    {
                        options.Authenticator.TokenLifespan = TimeSpan.FromHours(1);
                        options.ChangeEmail.TokenLifespan = TimeSpan.FromDays(1);
                        options.ChangePhoneNumber.TokenLifespan = TimeSpan.FromDays(1);
                        options.EmailConfirmation.TokenLifespan = TimeSpan.FromDays(2);
                        options.PasswordReset.TokenLifespan = TimeSpan.FromHours(2);
                    })
                .AddClaimsPrincipalFactory<CustomUserClaimsPrincipalFactory>()
                .AddUserManager<UserManager>()
                .AddSignInManager<SignInManager>()
                .AddRoleManager<RoleManager>()
                .BuildCustomServices();

            services.Configure<PasswordHasherOptions>(
                option =>
                {
                    option.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3;
                    option.IterationCount = HostingEnvironment.IsProduction() ? 100000 : 1;
                });

            services.AddMvc()
                .AddNewtonsoftJson(
                    options =>
                    {
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    })
                .AddDevTools<IdentityDbContextSeeder, IdentityDbContextCleaner>()
                .AddRazorPagesOptions(
                    options =>
                    {
                        options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                        options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                    })
                .AddFluentValidation(
                    config =>
                    {
                        config.RegisterValidatorsFromAssemblyContaining<Startup>();
                        config.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    });

            services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.ApiVersionReader = new HeaderApiVersionReader();
                });

            services.AddVersionedApiExplorer();

            services.AddAutoMapper(Assembly.GetAssembly(typeof(Startup)), Assembly.GetAssembly(typeof(IdentityDbContext)));

            services.AddIdentityServer(
                    options =>
                    {
                        options.IssuerUri = AppSettings.Endpoints.IdentityUrl;
                        options.Authentication.CookieLifetime = TimeSpan.FromHours(2);
                        options.Events.RaiseInformationEvents = true;
                        options.Events.RaiseSuccessEvents = true;
                        options.Events.RaiseFailureEvents = true;
                        options.Events.RaiseErrorEvents = true;
                        options.UserInteraction.LoginUrl = "/Account/Login";
                        options.UserInteraction.LoginReturnUrlParameter = "returnUrl";
                        options.UserInteraction.LogoutUrl = "/Account/Logout";
                    })
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                .AddInMemoryClients(IdentityServerConfig.GetClients(AppSettings))
                .AddCorsPolicyService<CustomCorsPolicyService>()
                .AddProfileService<CustomProfileService>()
                .AddAspNetIdentity<User>()
                .BuildCustomServices();

            services.AddMediatR(Assembly.GetAssembly(typeof(Startup)));

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(
                    options =>
                    {
                        options.ApiName = AppSettings.ApiResource.Name;
                        options.Authority = AppSettings.Endpoints.IdentityUrl;
                        options.RequireHttpsMetadata = false;
                        options.ApiSecret = "secret";
                    });

            services.AddSwagger(XmlCommentsFilePath, AppSettings, AppSettings);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AzureServiceBusModule<Startup>(Configuration.GetAzureServiceBusConnectionString()!, AppNames.IdentityApi));

            builder.RegisterModule<IdentityModule>();
        }

        public void Configure(IApplicationBuilder application, IServiceBusSubscriber subscriber, IApiVersionDescriptionProvider provider)
        {
            subscriber.UseIntegrationEventSubscriptions();

            if (HostingEnvironment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }
            else
            {
                //application.UseCustomExceptionHandler();
                application.UseExceptionHandler("/Home/Error");
                application.UseHsts();
            }

            application.UsePathBase(Configuration["ASPNETCORE_PATHBASE"]);

            application.UseHttpsRedirection();
            application.UseStaticFiles();

            // https://github.com/IdentityServer/IdentityServer4/issues/1331
            application.UseForwardedHeaders(
                new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                    RequireHeaderSymmetry = false
                });

            application.UseCookiePolicy();

            application.UseRouting();
            application.UseCors("default");

            application.UseAuthentication();
            application.UseIdentityServer();
            application.UseAuthorization();

            application.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapRazorPages();

                    endpoints.MapDefaultControllerRoute();

                    endpoints.MapConfigurationRoute<IdentityAppSettings>(AppSettings.ApiResource);

                    endpoints.MapHealthChecks(
                        "/liveness",
                        new HealthCheckOptions
                        {
                            Predicate = registration => registration.Name.Contains("liveness")
                        });

                    endpoints.MapHealthChecks(
                        "/health",
                        new HealthCheckOptions
                        {
                            Predicate = _ => true,
                            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                        });
                });

            application.UseSwagger(provider, AppSettings);
        }
    }
}
