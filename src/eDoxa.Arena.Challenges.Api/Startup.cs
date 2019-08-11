// Filename: Startup.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;

using Autofac;

using AutoMapper;

using eDoxa.Arena.Challenges.Api.Application.DelegatingHandlers;
using eDoxa.Arena.Challenges.Api.Application.Services;
using eDoxa.Arena.Challenges.Api.Extensions;
using eDoxa.Arena.Challenges.Api.Infrastructure;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Games.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.Modules;
using eDoxa.Seedwork.Application.Swagger.Extensions;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Monitoring;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Security.Constants;
using eDoxa.Seedwork.Security.Extensions;
using eDoxa.ServiceBus.Modules;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using static eDoxa.Seedwork.Security.IdentityServer.Resources.CustomApiResources;

namespace eDoxa.Arena.Challenges.Api
{
    public sealed class Startup
    {
        private static readonly string XmlCommentsFilePath = Path.Combine(
            AppContext.BaseDirectory,
            $"{typeof(Startup).GetTypeInfo().Assembly.GetName().Name}.xml"
        );

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            AppSettings = configuration.GetAppSettings<ArenaChallengesAppSettings>(ArenaChallengesApi);
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        private ArenaChallengesAppSettings AppSettings { get; }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppSettings<ArenaChallengesAppSettings>(Configuration);

            services.AddHealthChecks(AppSettings);

            services.AddCorsPolicy();

            services.AddDbContext<ArenaChallengesDbContext, ArenaChallengesDbContextData>(
                AppSettings.ConnectionStrings.SqlServer,
                Assembly.GetAssembly(typeof(Startup))
            );

            services.AddDistributedRedisCache(
                options =>
                {
                    options.Configuration = Configuration.GetConnectionString(CustomConnectionStrings.Redis);
                    options.InstanceName = HostingEnvironment.ApplicationName;
                }
            );

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddControllersAsServices()
                .AddFluentValidation(config => config.RunDefaultMvcValidationAfterFluentValidationExecutes = false);

            services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.ApiVersionReader = new HeaderApiVersionReader(CustomHeaderNames.Version);
                }
            );

            services.AddAutoMapper(Assembly.GetAssembly(typeof(Startup)), Assembly.GetAssembly(typeof(ArenaChallengesDbContext)));

            services.AddAuthentication(HostingEnvironment, AppSettings);

            // TODO: Use claims instead.
            services.AddTransient<IdentityDelegatingHandler>();

            services.AddHttpClient<IIdentityService, IdentityService>()
                .AddHttpMessageHandler<IdentityDelegatingHandler>()
                .AddPolicyHandler(HttpPolicies.GetRetryPolicy())
                .AddPolicyHandler(HttpPolicies.GetCircuitBreakerPolicy());

            services.AddArenaGames(Configuration);
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            this.ConfigureServices(services);

            services.AddSwagger(XmlCommentsFilePath, AppSettings);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<DomainEventModule>();

            builder.RegisterModule<RequestModule>();

            builder.RegisterModule(new ServiceBusModule<Startup>(AppSettings));

            builder.RegisterModule<ArenaChallengesApiModule>();
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseHealthChecks();

            application.UseCorsPolicy();

            application.UseCustomExceptionHandler();

            application.UseAuthentication();

            application.UseMvc();

            application.UseServiceBusSubscriber();
        }

        public void ConfigureDevelopment(IApplicationBuilder application, IApiVersionDescriptionProvider provider)
        {
            this.Configure(application);

            application.UseSwagger(provider, AppSettings);
        }
    }
}
