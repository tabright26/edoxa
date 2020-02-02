// Filename: Startup.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;

using Autofac;

using eDoxa.Grpc.Protos.Identity.Options;
using eDoxa.Identity.Api.Extensions;
using eDoxa.Identity.Api.Grpc.Services;
using eDoxa.Identity.Api.Infrastructure;
using eDoxa.Identity.Api.IntegrationEvents.Extensions;
using eDoxa.Identity.Infrastructure;
using eDoxa.Seedwork.Application.AutoMapper.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.FluentValidation;
using eDoxa.Seedwork.Application.Grpc.Extensions;
using eDoxa.Seedwork.Application.ProblemDetails.Extensions;
using eDoxa.Seedwork.Application.Swagger;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Monitoring;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Monitoring.HealthChecks.Extensions;
using eDoxa.Seedwork.Security.Cors.Extensions;
using eDoxa.Seedwork.Security.DataProtection.Extensions;
using eDoxa.Seedwork.Security.Hsts.Extensions;
using eDoxa.ServiceBus.Abstractions;
using eDoxa.ServiceBus.Azure.Extensions;
using eDoxa.ServiceBus.TestHelper.Extensions;

using FluentValidation;

using Hellang.Middleware.ProblemDetails;

using IdentityServer4.AccessTokenValidation;

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

            services.AddCustomIdentity();

            services.AddCustomCors();

            services.AddCustomGrpc();

            services.AddProblemDetails();

            services.AddCustomControllers<Startup>();

            services.AddCustomApiVersioning(new ApiVersion(1, 0));

            services.AddCustomAutoMapper(typeof(Startup), typeof(IdentityDbContext));

            services.AddMediatR(typeof(Startup));

            services.AddCustomIdentityServer(AppSettings);

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

                    endpoints.MapControllers();

                    endpoints.MapCustomHealthChecks();
                });

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

            services.AddCustomIdentity();

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
