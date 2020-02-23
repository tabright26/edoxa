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
using eDoxa.Identity.Api.Infrastructure.Data;
using eDoxa.Identity.Api.IntegrationEvents.Extensions;
using eDoxa.Identity.Infrastructure;
using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Application.Authorization.Extensions;
using eDoxa.Seedwork.Application.AutoMapper.Extensions;
using eDoxa.Seedwork.Application.Cors.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.FluentValidation;
using eDoxa.Seedwork.Application.Grpc.Extensions;
using eDoxa.Seedwork.Application.HealthChecks.Extensions;
using eDoxa.Seedwork.Application.HttpsPolicy.Extensions;
using eDoxa.Seedwork.Application.Middlewares.ProblemDetails.Extensions;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;
using eDoxa.Seedwork.Application.Swagger;
using eDoxa.Seedwork.Infrastructure.DataProtection.Extensions;
using eDoxa.Seedwork.Infrastructure.Extensions;
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IHostEnvironment Environment { get; }

        private IdentityAppSettings AppSettings => Configuration.Get<IdentityAppSettings>();
    }

    public partial class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions<IdentityAppSettings>().Bind(Configuration).ValidateDataAnnotations();

            services.Configure<IdentityApiOptions>(Configuration.GetSection("Api"));

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
                    options.IterationCount = Environment.IsProduction() ? 100000 : 1;
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

            services.AddCustomAuthorization();

            services.AddCustomApiVersioning(new ApiVersion(1, 0));

            services.AddCustomAutoMapper(typeof(Startup), typeof(IdentityDbContext));

            services.AddMediatR(typeof(Startup));

            services.AddCustomIdentityServer(Environment, AppSettings);

            //services.AddAuthentication().AddIdentityServerJwt();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerJwt()
                .AddIdentityServerAuthentication(
                    options =>
                    {
                        options.ApiName = ApiResources.IdentityApi.Name;
                        options.Authority = AppSettings.Authority.InternalUrl;
                        options.RequireHttpsMetadata = false;
                        options.ApiSecret = "secret";
                    });
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

            subscriber.UseIntegrationEventSubscriptions();
        }
    }

    public partial class Startup
    {
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            this.ConfigureServices(services);

            services.AddSwagger(XmlCommentsFilePath, ApiResources.IdentityApi, AppSettings.Authority);
        }

        public void ConfigureDevelopment(IApplicationBuilder application, IServiceBusSubscriber subscriber)
        {
            this.Configure(application, subscriber);

            application.UseSwagger(ApiResources.IdentityApi);
        }
    }

    public partial class Startup
    {
        public void ConfigureTestServices(IServiceCollection services)
        {
            services.Configure<IdentityAppSettings>(Configuration);

            services.Configure<IdentityApiOptions>(Configuration.GetSection("Api"));

            services.AddCustomDbContext<IdentityDbContext>(Configuration, Assembly.GetAssembly(typeof(Startup)));

            services.AddCustomIdentity();

            services.AddCustomCors();

            services.AddCustomGrpc();

            services.AddCustomProblemDetails();

            services.AddCustomControllers<Startup>();

            services.AddCustomAuthorization();

            services.AddCustomApiVersioning(new ApiVersion(1, 0));

            services.AddCustomAutoMapper(typeof(Startup), typeof(IdentityDbContext));

            services.AddMediatR(typeof(Startup));

            services.AddAuthentication();
        }

        public void ConfigureTestContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<IdentityModule>();

            builder.RegisterType<IdentityDbContextCleaner>().As<IDbContextCleaner>().InstancePerLifetimeScope();

            builder.RegisterMockServiceBusModule();
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
