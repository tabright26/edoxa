﻿// Filename: Startup.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Reflection;

using eDoxa.Autofac.Extensions;
using eDoxa.AutoMapper.Extensions;
using eDoxa.Identity.Api.Extensions;
using eDoxa.Identity.Application;
using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.DTO.Factories;
using eDoxa.Identity.Infrastructure;
using eDoxa.Monitoring.Extensions;
using eDoxa.Security.Extensions;
using eDoxa.Security.Resources;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.ServiceBus.Extensions;
using eDoxa.Swagger.Extensions;
using eDoxa.Versioning.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Identity.Api
{
    public sealed class Startup
    {
        private static readonly CustomApiResources.IdentityApi IdentityApi = new CustomApiResources.IdentityApi();

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

            services.AddIntegrationEventDbContext(Configuration, Assembly.GetAssembly(typeof(IdentityDbContext)));

            services.AddDbContext<IdentityDbContext, IdentityDbContextData>(Configuration);

            services.AddIdentityCore<User, Role, IdentityDbContext>();

            services.AddVersioning();

            services.AddAutoMapper(IdentityMapperFactory.Instance);

            services.AddMvcFilters();

            services.AddSwagger(Configuration, Environment, IdentityApi);

            services.AddCorsPolicy();

            services.AddServiceBus(Configuration);

            services.AddIdentityServerAuthentication(Configuration, Environment, IdentityApi);

            return services.Build<ApplicationModule>();
        }

        public void Configure(IApplicationBuilder application, IApiVersionDescriptionProvider provider)
        {
            application.UseHealthChecks();

            application.UseCorsPolicy();

            application.UseCustomExceptionHandler();

            application.UseAuthentication();

            application.UseStaticFiles();

            application.UseSwagger(Environment, provider, IdentityApi);

            application.UseMvc();

            application.UseIntegrationEventSubscriptions();
        }
    }
}