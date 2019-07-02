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
using System.Reflection;

using AutoMapper;

using eDoxa.Arena.Challenges.Api.Extensions;
using eDoxa.Arena.Challenges.Api.Infrastructure;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Extensions;
using eDoxa.IntegrationEvents.Extensions;
using eDoxa.Monitoring.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.Swagger.Extensions;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Security.Constants;
using eDoxa.Seedwork.Security.Extensions;
using eDoxa.Seedwork.Security.IdentityServer.Resources;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Arena.Challenges.Api
{
    public sealed class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        private IHostingEnvironment Environment { get; }

        private IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks(Configuration);

            services.AddEntityFrameworkSqlServer();

            services.AddIntegrationEventDbContext(Configuration, Assembly.GetAssembly(typeof(Startup)));

            services.AddDbContext<ChallengesDbContext, ChallengesDbContextData>(Configuration, Assembly.GetAssembly(typeof(Startup)));

            services.AddVersioning();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDistributedRedisCache(
                options =>
                {
                    options.Configuration = Configuration.GetConnectionString(CustomConnectionStrings.Redis);
                    options.InstanceName = Environment.ApplicationName;
                }
            );

            services.AddMvcFilters();

            services.AddSwagger(Configuration, Environment, CustomApiResources.ArenaChallenges);

            services.AddCorsPolicy();

            services.AddServiceBus(Configuration);

            services.AddAuthentication(Configuration, Environment, CustomApiResources.ArenaChallenges);

            services.AddArena(Configuration);

            return services.Build<ApiModule>();
        }

        public void Configure(IApplicationBuilder application, IApiVersionDescriptionProvider provider)
        {
            application.UseHealthChecks();

            application.UseCorsPolicy();

            application.UseCustomExceptionHandler();

            application.UseAuthentication(Environment);

            application.UseStaticFiles();

            application.UseSwagger(Environment, provider, CustomApiResources.ArenaChallenges);

            application.UseMvc();

            application.UseIntegrationEventSubscriptions();
        }
    }
}
