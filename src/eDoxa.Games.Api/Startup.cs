﻿// Filename: Startup.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;

using Autofac;

using eDoxa.Games.Api.Grpc.Services;
using eDoxa.Games.Api.Infrastructure;
using eDoxa.Games.Api.Infrastructure.Data;
using eDoxa.Games.Infrastructure;
using eDoxa.Games.LeagueOfLegends.Abstactions;
using eDoxa.Grpc.Protos.Games.Options;
using eDoxa.Seedwork.Application.Autofac.Extensions;
using eDoxa.Seedwork.Application.AutoMapper.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.FluentValidation;
using eDoxa.Seedwork.Application.Grpc.Extensions;
using eDoxa.Seedwork.Application.ProblemDetails.Extensions;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;
using eDoxa.Seedwork.Application.Swagger;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Infrastructure.Redis.Extensions;
using eDoxa.Seedwork.Monitoring;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Monitoring.HealthChecks.Extensions;
using eDoxa.Seedwork.Security.Cors.Extensions;
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

namespace eDoxa.Games.Api
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
            AppSettings = configuration.GetAppSettings<GamesAppSettings>(GamesApi);
        }

        private GamesAppSettings AppSettings { get; }

        public IConfiguration Configuration { get; }
    }

    public partial class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppSettings<GamesAppSettings>(Configuration);

            services.Configure<GamesApiOptions>(Configuration.GetSection("Api"));

            services.AddHealthChecks()
                .AddCustomSelfCheck()
                .AddCustomIdentityServer(AppSettings)
                .AddCustomAzureKeyVault(Configuration)
                .AddCustomRedis(Configuration)
                .AddCustomSqlServer(Configuration)
                .AddCustomAzureServiceBusTopic(Configuration);

            services.AddCustomDbContext<GamesDbContext>(Configuration, Assembly.GetAssembly(typeof(Startup)));

            services.AddCustomRedis(Configuration);

            services.AddCustomCors();

            services.AddCustomGrpc();

            services.AddCustomProblemDetails();

            services.AddCustomControllers<Startup>();

            services.AddCustomApiVersioning(new ApiVersion(1, 0));

            services.AddCustomAutoMapper(typeof(Startup), typeof(GamesDbContext));

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
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAzureServiceBusModule<Startup>(AppServices.GamesApi);

            builder.RegisterModule<GamesModule>();
        }

        public void Configure(IApplicationBuilder application)
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
                    endpoints.MapGrpcService<GameGrpcService>();

                    endpoints.MapControllers();

                    endpoints.MapCustomHealthChecks();
                });
        }
    }

    public partial class Startup
    {
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            this.ConfigureServices(services);

            services.AddSwagger(XmlCommentsFilePath, AppSettings, AppSettings);
        }

        public void ConfigureDevelopment(IApplicationBuilder application)
        {
            this.Configure(application);

            application.UseSwagger(AppSettings);
        }
    }

    public partial class Startup
    {
        public void ConfigureTestServices(IServiceCollection services)
        {
            services.AddAppSettings<GamesAppSettings>(Configuration);

            services.Configure<GamesApiOptions>(Configuration.GetSection("Api"));

            services.AddCustomDbContext<GamesDbContext>(Configuration, Assembly.GetAssembly(typeof(Startup)));

            services.AddCustomRedis(Configuration);

            services.AddCustomCors();

            services.AddCustomGrpc();

            services.AddCustomProblemDetails();

            services.AddCustomControllers<Startup>();

            services.AddCustomApiVersioning(new ApiVersion(1, 0));

            services.AddCustomAutoMapper(typeof(Startup), typeof(GamesDbContext));

            services.AddMediatR(typeof(Startup));

            services.AddAuthentication();
        }

        public void ConfigureTestContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<GamesModule>();

            builder.RegisterType<GamesDbContextCleaner>().As<IDbContextCleaner>().InstancePerLifetimeScope();

            builder.RegisterMockServiceBusModule();

            builder.RegisterMock<ILeagueOfLegendsService>();
        }

        public void ConfigureTest(IApplicationBuilder application)
        {
            application.UseProblemDetails();

            application.UseCustomPathBase();

            application.UseRouting();
            application.UseCustomCors();

            application.UseAuthentication();
            application.UseAuthorization();

            application.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GameGrpcService>();

                endpoints.MapControllers();
            });
        }
    }
}
