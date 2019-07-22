// Filename: Startup.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;

using AutoMapper;

using eDoxa.Arena.Challenges.Api.Application.Adapters;
using eDoxa.Arena.Challenges.Api.Application.DelegatingHandlers;
using eDoxa.Arena.Challenges.Api.Application.Factories;
using eDoxa.Arena.Challenges.Api.Application.Services;
using eDoxa.Arena.Challenges.Api.Application.Strategies;
using eDoxa.Arena.Challenges.Api.Extensions;
using eDoxa.Arena.Challenges.Api.Infrastructure;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data;
using eDoxa.Arena.Challenges.Api.Infrastructure.Queries;
using eDoxa.Arena.Challenges.Domain.Adapters;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.Domain.Strategies;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Repositories;
using eDoxa.Arena.Extensions;
using eDoxa.IntegrationEvents.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.Swagger;
using eDoxa.Seedwork.Application.Swagger.Extensions;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Monitoring;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Security.Constants;
using eDoxa.Seedwork.Security.Extensions;

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
    public class Startup
    {
        private static readonly string XmlCommentsFilePath = Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).GetTypeInfo().Assembly.GetName().Name}.xml");

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        private AppSettings AppSettings { get; set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks(Configuration);

            services.AddCorsPolicy();

            services.AddEntityFrameworkSqlServer();

            services.AddIntegrationEventDbContext(Configuration, Assembly.GetAssembly(typeof(Startup)));

            services.AddDbContext<ArenaChallengesDbContext, ArenaChallengesDbContextData>(Configuration, Assembly.GetAssembly(typeof(Startup)));

            services.AddDistributedRedisCache(
                options =>
                {
                    options.Configuration = Configuration.GetConnectionString(CustomConnectionStrings.Redis);
                    options.InstanceName = HostingEnvironment.ApplicationName;
                }
            );

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VV");

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

            AppSettings = services.ConfigureBusinessServices(Configuration, ArenaChallengesApi);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            if (AppSettings.IsValid())
            {
                if (AppSettings.Swagger.Enabled)
                {
                    services.AddSwaggerGen(options =>
                    {
                        var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                        foreach (var description in provider.ApiVersionDescriptions)
                        {
                            options.SwaggerDoc(description.GroupName, description.CreateInfoForApiVersion(AppSettings));
                        }

                        options.IncludeXmlComments(XmlCommentsFilePath);

                        options.AddSecurityDefinition(AppSettings);

                        options.AddFilters();
                    });
                }
            }

            services.AddServiceBus(Configuration);

            services.AddAuthentication(Configuration, HostingEnvironment, ArenaChallengesApi);

            services.AddTransient<IdentityDelegatingHandler>();

            services.AddHttpClient<IIdentityService, IdentityService>()
                .AddHttpMessageHandler<IdentityDelegatingHandler>()
                .AddPolicyHandler(HttpPolicies.GetRetryPolicy())
                .AddPolicyHandler(HttpPolicies.GetCircuitBreakerPolicy());

            services.AddArenaServices(Configuration);

            // Repositories
            services.AddScoped<IChallengeRepository, ChallengeRepository>();

            // Queries
            services.AddScoped<IChallengeQuery, ChallengeQuery>();
            services.AddScoped<IParticipantQuery, ParticipantQuery>();
            services.AddScoped<IMatchQuery, MatchQuery>();

            // Services
            services.AddScoped<IChallengeService, ChallengeService>();

            // Strategies
            services.AddTransient<IScoringStrategy, LeagueOfLegendsScoringStrategy>();

            // Adapters
            services.AddTransient<IGameReferencesAdapter, LeagueOfLegendsGameReferencesAdapter>();
            services.AddTransient<IMatchAdapter, LeagueOfLegendsMatchAdapter>();

            // Factories
            services.AddSingleton<IScoringFactory, ScoringFactory>();
            services.AddSingleton<IGameReferencesFactory, GameReferencesFactory>();
            services.AddSingleton<IMatchFactory, MatchFactory>();

            return this.BuildModule(services);
        }

        public void Configure(IApplicationBuilder application, IApiVersionDescriptionProvider provider)
        {
            application.UseHealthChecks();

            application.UseCorsPolicy();

            application.UseCustomExceptionHandler();

            application.UseAuthentication(HostingEnvironment);

            if (AppSettings.IsValid())
            {
                if (AppSettings.Swagger.Enabled)
                {
                    application.UseSwagger();

                    application.UseSwaggerUI(options =>
                    {
                        foreach (var description in provider.ApiVersionDescriptions)
                        {
                            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                        }

                        options.RoutePrefix = string.Empty;

                        options.OAuthClientId(AppSettings.ApiResource.SwaggerClientId());

                        options.OAuthAppName(AppSettings.ApiResource.SwaggerClientName());

                        options.DefaultModelExpandDepth(0);

                        options.DefaultModelsExpandDepth(-1);
                    });
                }
            }

            application.UseMvc();

            application.UseIntegrationEventSubscriptions();
        }

        // TODO: Required by integration and functional tests.
        protected virtual IServiceProvider BuildModule(IServiceCollection services)
        {
            return services.Build<ArenaChallengesModule>();
        }
    }
}
