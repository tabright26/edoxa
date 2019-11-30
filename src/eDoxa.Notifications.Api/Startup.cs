// Filename: Startup.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;

using Autofac;

using AutoMapper;

using eDoxa.Notifications.Api.Infrastructure;
using eDoxa.Notifications.Api.Infrastructure.Data;
using eDoxa.Notifications.Api.IntegrationEvents.Extensions;
using eDoxa.Notifications.Infrastructure;
using eDoxa.Seedwork.Application.DevTools.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.FluentValidation;
using eDoxa.Seedwork.Application.Swagger;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Monitoring;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Monitoring.HealthChecks.Extensions;
using eDoxa.Seedwork.Security.Cors.Extensions;
using eDoxa.ServiceBus.Abstractions;
using eDoxa.ServiceBus.Azure.Modules;
using eDoxa.Storage.Azure.Extensions;

using FluentValidation;

using Hellang.Middleware.ProblemDetails;

using IdentityServer4.AccessTokenValidation;

using MediatR;

using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using static eDoxa.Seedwork.Security.ApiResources;

namespace eDoxa.Notifications.Api
{
    public class Startup
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
            AppSettings = configuration.GetAppSettings<NotificationsAppSettings>(NotificationsApi);
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment HostingEnvironment { get; }

        private NotificationsAppSettings AppSettings { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppSettings<NotificationsAppSettings>(Configuration);

            services.AddHealthChecks()
                .AddCustomSelfCheck()
                .AddIdentityServer(AppSettings)
                .AddAzureKeyVault(Configuration)
                .AddSqlServer(Configuration)
                .AddAzureBlobStorage(Configuration)
                .AddAzureServiceBusTopic(Configuration);

            services.AddDbContext<NotificationsDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetSqlServerConnectionString()!,
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(Startup))!.GetName().Name);
                        sqlServerOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                    }));

            services.AddAzureStorage(Configuration.GetAzureBlobStorageConnectionString()!);

            services.AddCustomCors();

            services.AddCustomProblemDetails();

            services.AddCustomControllers<Startup>().AddDevTools<NotificationsDbContextSeeder, NotificationsDbContextCleaner>();

            services.AddCustomApiVersioning(new ApiVersion(1, 0));

            services.AddAutoMapper(typeof(Startup), typeof(NotificationsDbContext));

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
            builder.RegisterModule(new AzureServiceBusModule<Startup>(Configuration.GetAzureServiceBusConnectionString()!, AppNames.NotificationsApi));

            builder.RegisterModule<NotificationsModule>();
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

                    endpoints.MapConfigurationRoute<NotificationsAppSettings>(AppSettings.ApiResource);

                    endpoints.MapCustomHealthChecks();
                });

            application.UseSwagger(AppSettings);

            subscriber.UseIntegrationEventSubscriptions();
        }
    }
}
