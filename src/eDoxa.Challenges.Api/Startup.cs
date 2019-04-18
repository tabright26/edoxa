﻿// Filename: Startup.cs
// Date Created: 2019-04-12
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Reflection;

using eDoxa.Autofac.Extensions;
using eDoxa.AutoMapper.Extensions;
using eDoxa.Challenges.Api.Extensions;
using eDoxa.Challenges.Application;
using eDoxa.Challenges.DTO.Factories;
using eDoxa.Challenges.Infrastructure;
using eDoxa.Monitoring.Extensions;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.ServiceBus.Extensions;
using eDoxa.Swagger.Extensions;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Challenges.Api
{
    public sealed class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        private IHostingEnvironment Environment { get; }

        private IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks(Configuration);

            services.AddEntityFrameworkSqlServer();            

            services.AddIntegrationEventDbContext(Configuration, Assembly.GetAssembly(typeof(ChallengesDbContext)));

            services.AddDbContext<ChallengesDbContext>(Configuration);

            services.AddVersioning(new ApiVersion(1, 0));

            services.AddAutoMapper(ChallengesMapperFactory.Instance);

            services.AddMvcWithApiBehavior();

            services.AddSwagger(Configuration, Environment, Assembly.GetExecutingAssembly());

            services.AddCorsPolicy();

            services.AddServiceBus(Configuration);

            services.AddEventBus(Configuration);

            services.AddAuthentication(
                        options =>
                        {
                            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        }
                    )
                    .AddJwtBearer(
                        options =>
                        {
                            options.Audience = Configuration["ApiResource:Name"];
                            options.Authority = Configuration["IdentityServer:Url"];
                            options.RequireHttpsMetadata = false;
                        }
                    );

            return services.Build<ApplicationModule>();
        }

        public void Configure(IApplicationBuilder application, IApiVersionDescriptionProvider provider)
        {
            application.UseHealthChecks();

            application.UseCorsPolicy();

            application.UseAuthentication();

            application.UseStaticFiles();

            application.UseSwagger(Configuration, Environment, provider);

            application.UseStatusCodePagesWithRedirects("~/swagger");

            application.UseMvcWithDefaultRoute();

            application.UseIntegrationEventSubscriptions();
        }
    }
}