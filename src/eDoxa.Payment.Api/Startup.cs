// Filename: Startup.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;

using Autofac;

using AutoMapper;

using eDoxa.Payment.Api.Areas.Stripe.Extensions;
using eDoxa.Payment.Api.Infrastructure;
using eDoxa.Payment.Api.Infrastructure.Data;
using eDoxa.Payment.Api.IntegrationEvents.Extensions;
using eDoxa.Payment.Infrastructure;
using eDoxa.Seedwork.Application.DevTools.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.Validations;
using eDoxa.Seedwork.Infrastructure.Extensions;
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

namespace eDoxa.Payment.Api
{
    public class Startup
    {
        private const string AzureServiceBusDiscriminator = "payment";

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
            AppSettings = configuration.GetAppSettings<PaymentAppSettings>(ApiResources.PaymentApi);
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public PaymentAppSettings AppSettings { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddStripe(Configuration);

            services.AddAppSettings<PaymentAppSettings>(Configuration);

            services.AddHealthChecks()
                .AddCheck("liveness", () => HealthCheckResult.Healthy())
                .AddIdentityServer(AppSettings)
                .AddAzureKeyVault(Configuration)
                .AddSqlServer(Configuration)
                .AddAzureServiceBusTopic(Configuration);

            services.AddDbContext<PaymentDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetSqlServerConnectionString()!,
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
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddDevTools<PaymentDbContextSeeder, PaymentDbContextCleaner>()
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

            services.AddAutoMapper(Assembly.GetAssembly(typeof(Startup)), Assembly.GetAssembly(typeof(PaymentDbContext)));

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
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            this.ConfigureServices(services);

            services.AddSwagger(XmlCommentsFilePath, AppSettings, AppSettings);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AzureServiceBusModule<Startup>(Configuration.GetAzureServiceBusConnectionString()!, AzureServiceBusDiscriminator));

            builder.RegisterModule<PaymentModule>();
        }

        public void Configure(IApplicationBuilder application, IServiceBusSubscriber subscriber)
        {
            subscriber.UseIntegrationEventSubscriptions();

            application.UseStripe(Configuration);

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
        }

        public void ConfigureDevelopment(IApplicationBuilder application, IServiceBusSubscriber subscriber, IApiVersionDescriptionProvider provider)
        {
            this.Configure(application, subscriber);

            application.UseSwagger(provider, AppSettings);
        }
    }
}
