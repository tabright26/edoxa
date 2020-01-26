// Filename: Startup.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;

using Autofac;

using eDoxa.Cashier.Api.Infrastructure;
using eDoxa.Cashier.Api.IntegrationEvents.Extensions;
using eDoxa.Cashier.Api.Services;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Grpc.Protos.Cashier.Options;
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
using eDoxa.ServiceBus.Abstractions;
using eDoxa.ServiceBus.Azure.Extensions;
using eDoxa.ServiceBus.TestHelper.Extensions;

using FluentValidation;

using Hellang.Middleware.ProblemDetails;

using IdentityServer4.AccessTokenValidation;

using MediatR;

using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using static eDoxa.Seedwork.Security.ApiResources;

namespace eDoxa.Cashier.Api
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

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettings = configuration.GetAppSettings<CashierAppSettings>(CashierApi);
        }

        public IConfiguration Configuration { get; }

        private CashierAppSettings AppSettings { get; }
    }

    public partial class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppSettings<CashierAppSettings>(Configuration);

            services.Configure<CashierApiOptions>(Configuration.GetSection("Api"));
            
            services.AddHealthChecks()
                .AddCustomSelfCheck()
                .AddCustomIdentityServer(AppSettings)
                .AddCustomAzureKeyVault(Configuration)
                .AddCustomSqlServer(Configuration)
                .AddCustomAzureServiceBusTopic(Configuration);

            services.AddCustomDbContext<CashierDbContext>(Configuration, Assembly.GetAssembly(typeof(Startup)));

            services.AddCustomCors();

            services.AddCustomGrpc();

            services.AddCustomProblemDetails();

            services.AddCustomControllers<Startup>();

            services.AddCustomApiVersioning(new ApiVersion(1, 0));

            services.AddCustomAutoMapper(typeof(Startup));

            services.AddMediatR(typeof(Startup));

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
            builder.RegisterAzureServiceBusModule<Startup>(AppServices.CashierApi);

            builder.RegisterModule<CashierModule>();
        }

        public void Configure(IApplicationBuilder application, IServiceBusSubscriber subscriber)
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
                    endpoints.MapGrpcService<CashierGrpcService>();

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
            services.AddAppSettings<CashierAppSettings>(Configuration);

            services.Configure<CashierApiOptions>(Configuration.GetSection("Api"));
            
            services.AddCustomDbContext<CashierDbContext>(Configuration, Assembly.GetAssembly(typeof(Startup)));

            services.AddCustomCors();

            services.AddCustomGrpc();

            services.AddCustomProblemDetails();

            services.AddCustomControllers<Startup>();

            services.AddCustomApiVersioning(new ApiVersion(1, 0));

            services.AddCustomAutoMapper(typeof(Startup), typeof(CashierDbContext));

            services.AddMediatR(typeof(Startup));

            services.AddAuthentication();
        }

        public void ConfigureTestContainer(ContainerBuilder builder)
        {
            builder.RegisterMockServiceBusModule();

            builder.RegisterModule<CashierModule>();
        }

        public void ConfigureTest(IApplicationBuilder application, IServiceBusSubscriber subscriber)
        {
            application.UseProblemDetails();

            application.UseCustomPathBase();

            application.UseRouting();
            application.UseCustomCors();

            application.UseAuthentication();
            application.UseAuthorization();

            application.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<CashierGrpcService>();

                endpoints.MapControllers();
            });

            subscriber.UseIntegrationEventSubscriptions();
        }
    }
}
