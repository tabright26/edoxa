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

using AutoMapper;

using eDoxa.Cashier.Api.Areas.Accounts;
using eDoxa.Cashier.Api.Infrastructure;
using eDoxa.Cashier.Api.Infrastructure.Data;
using eDoxa.Cashier.Api.IntegrationEvents.Extensions;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Seedwork.Application.DevTools.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.FluentValidation;
using eDoxa.Seedwork.Application.Swagger;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Monitoring;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Monitoring.HealthChecks.Extensions;
using eDoxa.Seedwork.Security;
using eDoxa.Seedwork.Security.Cors.Extensions;
using eDoxa.ServiceBus.Abstractions;
using eDoxa.ServiceBus.Azure.Modules;

using FluentValidation;

using Hellang.Middleware.ProblemDetails;

using IdentityServer4.AccessTokenValidation;

using MediatR;

using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using static eDoxa.Seedwork.Security.ApiResources;

namespace eDoxa.Cashier.Api
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

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettings = configuration.GetAppSettings<CashierAppSettings>(CashierApi);
        }

        public IConfiguration Configuration { get; }

        private CashierAppSettings AppSettings { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppSettings<CashierAppSettings>(Configuration);

            services.Configure<BundlesOptions>(Configuration.GetSection("Bundles"));

            services.AddHealthChecks()
                .AddCustomSelfCheck()
                .AddIdentityServer(AppSettings)
                .AddAzureKeyVault(Configuration)
                .AddSqlServer(Configuration)
                .AddAzureServiceBusTopic(Configuration)
                .AddUrlGroup(AppSettings.Endpoints.PaymentUrl, "paymentapi");

            services.AddDbContext<CashierDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetSqlServerConnectionString(),
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(Startup))!.GetName().Name);
                        sqlServerOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                    }));

            services.AddCustomCors();

            services.AddCustomProblemDetails();

            services.AddCustomControllers<Startup>().AddDevTools<CashierDbContextSeeder, CashierDbContextCleaner>();

            services.AddCustomApiVersioning(new ApiVersion(1, 0));

            services.AddAutoMapper(typeof(Startup), typeof(CashierDbContext));

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

            services.AddSwagger(
                XmlCommentsFilePath,
                AppSettings,
                AppSettings,
                Scopes.PaymentApi);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AzureServiceBusModule<Startup>(Configuration.GetAzureServiceBusConnectionString()!, AppNames.CashierApi));

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
                    endpoints.MapControllers();

                    endpoints.MapConfigurationRoute<CashierAppSettings>(AppSettings.ApiResource);

                    endpoints.MapCustomHealthChecks();
                });

            application.UseSwagger(AppSettings);

            subscriber.UseIntegrationEventSubscriptions();
        }
    }
}
