// Filename: Startup.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Reflection;

using Autofac;

using AutoMapper;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.DelegatingHandlers;
using eDoxa.Arena.Challenges.Api.Areas.Challenges.Services;
using eDoxa.Arena.Challenges.Api.Extensions;
using eDoxa.Arena.Challenges.Api.Infrastructure;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Games.Extensions;
using eDoxa.Mediator;
using eDoxa.Seedwork.Application.DevTools.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.Validations;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.ServiceBus.Modules;

using FluentValidation;
using FluentValidation.AspNetCore;

using HealthChecks.UI.Client;

using IdentityServer4.AccessTokenValidation;

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

using Newtonsoft.Json;

using Polly;
using Polly.Extensions.Http;

using static eDoxa.Seedwork.Security.ApiResources;

using ConnectionStrings = eDoxa.Seedwork.Infrastructure.ConnectionStrings;

namespace eDoxa.Arena.Challenges.Api
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
            AppSettings = configuration.GetAppSettings<ArenaChallengesAppSettings>(ArenaChallengesApi);
        }

        private ArenaChallengesAppSettings AppSettings { get; }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppSettings<ArenaChallengesAppSettings>(Configuration);

            services.AddHealthChecks(AppSettings);

            services.AddDbContext<ArenaChallengesDbContext>(
                options => options.UseSqlServer(
                    AppSettings.ConnectionStrings.SqlServer,
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(Startup)).GetName().Name);
                        sqlServerOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                    }));

            services.AddDistributedRedisCache(
                options =>
                {
                    options.Configuration = Configuration.GetConnectionString(ConnectionStrings.Redis);
                    options.InstanceName = HostingEnvironment.ApplicationName;
                });

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
                .AddDevTools<ArenaChallengesDbContextSeeder, ArenaChallengesDbContextCleaner>()
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

            services.AddAutoMapper(Assembly.GetAssembly(typeof(Startup)), Assembly.GetAssembly(typeof(ArenaChallengesDbContext)));

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(
                    options =>
                    {
                        options.ApiName = AppSettings.ApiResource.Name;
                        options.Authority = AppSettings.Authority.PrivateUrl;
                        options.RequireHttpsMetadata = false;
                        options.ApiSecret = "secret";
                    });

            // TODO: Use claims instead.
            services.AddTransient<IdentityDelegatingHandler>();

            services.AddHttpClient<IIdentityService, IdentityService>()
                .AddHttpMessageHandler<IdentityDelegatingHandler>()
                .AddPolicyHandler(
                    HttpPolicyExtensions.HandleTransientHttpError()
                        .OrResult(message => message.StatusCode == HttpStatusCode.NotFound)
                        .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
                .AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError().CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddArenaGames(Configuration);
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            this.ConfigureServices(services);

            services.AddSwagger(XmlCommentsFilePath, AppSettings, AppSettings);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<DomainEventModule<Startup>>();

            builder.RegisterModule(new ServiceBusModule<Startup>(AppSettings));

            builder.RegisterModule<ArenaChallengesApiModule>();
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseServiceBusSubscriber();

            application.UseCustomExceptionHandler();

            application.UsePathBase(Configuration["ASPNETCORE_PATH_BASE"]);

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

        public void ConfigureDevelopment(IApplicationBuilder application, IApiVersionDescriptionProvider provider)
        {
            this.Configure(application);

            application.UseSwagger(provider, AppSettings);
        }
    }
}
