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

using eDoxa.Cashier.Api.Areas.Accounts;
using eDoxa.Cashier.Api.Infrastructure;
using eDoxa.Cashier.Api.Infrastructure.Data;
using eDoxa.Cashier.Api.IntegrationEvents.Extensions;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Seedwork.Application.DevTools.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.Validations;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Monitoring;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Security;
using eDoxa.ServiceBus.Abstractions;
using eDoxa.ServiceBus.Azure.Modules;

using FluentValidation;
using FluentValidation.AspNetCore;

using HealthChecks.UI.Client;

using IdentityServer4.AccessTokenValidation;

using MediatR;

using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using Newtonsoft.Json;

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

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            AppSettings = configuration.GetAppSettings<CashierAppSettings>(CashierApi);
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        private CashierAppSettings AppSettings { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppSettings<CashierAppSettings>(Configuration);

            services.Configure<BundlesOptions>(Configuration.GetSection("Bundles"));

            services.AddHealthChecks()
                .AddCheck("liveness", () => HealthCheckResult.Healthy())
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
                        sqlServerOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(Startup)).GetName().Name);
                        sqlServerOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                    }));

            services.AddCors(
                options =>
                {
                    options.AddPolicy("default", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(_ => true));
                });

            services.AddMvc(
                    options =>
                    {
                        options.Filters.Add(new ProducesAttribute("application/json"));
                    })
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddDevTools<CashierDbContextSeeder, CashierDbContextCleaner>()
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

            services.AddAutoMapper(Assembly.GetAssembly(typeof(Startup)), Assembly.GetAssembly(typeof(CashierDbContext)));

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

        public void Configure(IApplicationBuilder application, IServiceBusSubscriber subscriber, IApiVersionDescriptionProvider provider)
        {
            subscriber.UseIntegrationEventSubscriptions();

            application.UseCustomExceptionHandler();

            application.UsePathBase(Configuration["ASPNETCORE_PATHBASE"]);

            application.UseCors("default");

            application.UseAuthentication();

            application.UseMvc();

            application.UseHealthChecks(
                "/liveness",
                new HealthCheckOptions
                {
                    Predicate = registration => registration.Name.Contains("liveness")
                });

            application.UseHealthChecks(
                "/health",
                new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

            application.UseSwagger(provider, AppSettings);
        }
    }
}
