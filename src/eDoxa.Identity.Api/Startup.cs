// Filename: Startup.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;

using Autofac;

using eDoxa.Grpc.Protos.Identity.Options;
using eDoxa.Identity.Api.Application.Services;
using eDoxa.Identity.Api.Extensions;
using eDoxa.Identity.Api.Infrastructure;
using eDoxa.Identity.Api.IntegrationEvents.Extensions;
using eDoxa.Identity.Api.Services;
using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.Infrastructure;
using eDoxa.Seedwork.Application.AutoMapper.Extensions;
using eDoxa.Seedwork.Application.DevTools.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.FluentValidation;
using eDoxa.Seedwork.Application.Grpc.Extensions;
using eDoxa.Seedwork.Application.ProblemDetails.Extensions;
using eDoxa.Seedwork.Application.Swagger;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Monitoring;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Monitoring.HealthChecks.Extensions;
using eDoxa.Seedwork.Security;
using eDoxa.Seedwork.Security.Cors.Extensions;
using eDoxa.Seedwork.Security.DataProtection.Extensions;
using eDoxa.Seedwork.Security.Hsts.Extensions;
using eDoxa.ServiceBus.Abstractions;
using eDoxa.ServiceBus.Azure.Extensions;
using eDoxa.ServiceBus.TestHelper.Extensions;

using FluentValidation;
using FluentValidation.AspNetCore;

using Hellang.Middleware.ProblemDetails;

using IdentityModel;

using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Services;

using MediatR;

using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using static eDoxa.Seedwork.Security.ApiResources;

namespace eDoxa.Identity.Api
{
    public partial class Startup
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
    }

    public partial class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppSettings<IdentityAppSettings>(Configuration);

            services.Configure<IdentityApiOptions>(Configuration.GetSection("Api"));
            
            services.Configure<AdminOptions>(Configuration.GetSection("Admin"));

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                    options.Secure = CookieSecurePolicy.SameAsRequest;
                });

            services.Configure<PasswordHasherOptions>(
                options =>
                {
                    options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3;
                    options.IterationCount = HostingEnvironment.IsProduction() ? 100000 : 1;
                });

            services.AddHealthChecks()
                .AddCustomSelfCheck()
                .AddCustomAzureKeyVault(Configuration)
                .AddCustomSqlServer(Configuration)
                .AddCustomRedis(Configuration)
                .AddCustomAzureServiceBusTopic(Configuration);

            services.AddCustomDbContext<IdentityDbContext>(Configuration, Assembly.GetAssembly(typeof(Startup)));

            services.AddCustomDataProtection(Configuration, AppServices.IdentityApi);

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
                .AddRoleManager<RoleService>();

            services.AddScoped<UserRepository>();
            services.AddScoped<CustomUserClaimsPrincipalFactory>();
            services.AddScoped<UserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<SignInService>();
            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<RoleService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddCustomCors();

            services.AddCustomGrpc();

            services.AddProblemDetails();

            services.AddMvc()
                .AddCustomNewtonsoftJson()
                .AddDevTools()
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

            services.AddCustomApiVersioning(new ApiVersion(1, 0));

            services.AddCustomAutoMapper(typeof(Startup), typeof(IdentityDbContext));

            services.AddMediatR(typeof(Startup));

            services.AddIdentityServer(
                    options =>
                    {
                        options.IssuerUri = AppSettings.Endpoints.IdentityUrl;
                        options.Authentication.CookieLifetime = TimeSpan.FromHours(2);
                        options.Events.RaiseInformationEvents = true;
                        options.Events.RaiseSuccessEvents = true;
                        options.Events.RaiseFailureEvents = true;
                        options.Events.RaiseErrorEvents = true;
                        options.UserInteraction.LoginUrl = $"{AppSettings.WebSpaUrl}/login";
                        options.UserInteraction.LoginReturnUrlParameter = "returnUrl";
                        options.UserInteraction.LogoutUrl = $"{AppSettings.WebSpaUrl}/logout";
                    })
                .AddApiAuthorization<User, IdentityDbContext>(
                    options =>
                    {
                        options.IdentityResources.Clear();
                        options.IdentityResources.AddRange(IdentityServerConfig.GetIdentityResources().ToArray());

                        options.ApiResources.Clear();
                        options.ApiResources.AddRange(IdentityServerConfig.GetApiResources().ToArray());

                        options.Clients.Clear();
                        options.Clients.AddRange(IdentityServerConfig.GetClients(AppSettings).ToArray());
                    })
                .AddProfileService<CustomProfileService>();

            services.AddTransient<IProfileService, CustomProfileService>();
            var cors = new DefaultCorsPolicyService(new LoggerFactory().CreateLogger<DefaultCorsPolicyService>())
            {
                AllowAll = true
            };
            services.AddSingleton<ICorsPolicyService>(cors);
            services.AddTransient<IReturnUrlParser, Application.Services.ReturnUrlParser>();

            //services.AddAuthentication().AddIdentityServerJwt();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerJwt()
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
            builder.RegisterAzureServiceBusModule<Startup>(AppServices.IdentityApi);

            builder.RegisterModule<IdentityModule>();
        }

        public void Configure(IApplicationBuilder application, IServiceBusSubscriber subscriber)
        {
            application.UseForwardedHeaders();

            application.UseCustomMvcOrApiExceptionHandler();

            application.UseCustomHsts();

            application.UseCustomPathBase();

            application.UseHttpsRedirection();
            application.UseStaticFiles();
            application.UseCookiePolicy();

            application.UseRouting();
            application.UseCustomCors();

            application.UseAuthentication();
            application.UseIdentityServer();
            application.UseAuthorization();

            application.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapGrpcService<IdentityGrpcService>();

                    endpoints.MapRazorPages();

                    endpoints.MapDefaultControllerRoute();

                    endpoints.MapConfigurationRoute<IdentityAppSettings>(AppSettings.ApiResource);

                    endpoints.MapCustomHealthChecks();
                });

            application.UseAuthenticationLoginRedirects();

            application.UseSwagger(AppSettings);

            subscriber.UseIntegrationEventSubscriptions();
        }
    }

    public partial class Startup
    {
        public void ConfigureTestServices(IServiceCollection services)
        {
            services.AddAppSettings<IdentityAppSettings>(Configuration);

            services.Configure<IdentityApiOptions>(Configuration.GetSection("Api"));

            services.AddCustomDbContext<IdentityDbContext>(Configuration, Assembly.GetAssembly(typeof(Startup)));

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
                .AddRoleManager<RoleService>();

            services.AddScoped<UserRepository>();
            services.AddScoped<CustomUserClaimsPrincipalFactory>();
            services.AddScoped<UserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<SignInService>();
            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<RoleService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddCustomCors();

            services.AddCustomGrpc();

            services.AddCustomProblemDetails();

            services.AddCustomControllers<Startup>();

            services.AddCustomApiVersioning(new ApiVersion(1, 0));

            services.AddCustomAutoMapper(typeof(Startup), typeof(IdentityDbContext));

            services.AddMediatR(typeof(Startup));

            services.AddAuthentication();
        }

        public void ConfigureTestContainer(ContainerBuilder builder)
        {
            builder.RegisterMockServiceBusModule();

            builder.RegisterModule<IdentityModule>();
        }

        public void ConfigureTest(IApplicationBuilder application, IServiceBusSubscriber subscriber)
        {
            application.UseProblemDetails();

            application.UseCustomPathBase();

            application.UseRouting();
            application.UseCustomCors();

            application.UseAuthentication();
            application.UseAuthorization();

            application.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapGrpcService<IdentityGrpcService>();

                    endpoints.MapControllers();
                });

            subscriber.UseIntegrationEventSubscriptions();
        }
    }
}
