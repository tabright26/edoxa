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

using eDoxa.Cashier.Api.Extensions;
using eDoxa.Cashier.Api.Infrastructure;
using eDoxa.Cashier.Api.Infrastructure.Data;
using eDoxa.Cashier.Infrastructure;
using eDoxa.IntegrationEvents.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.Swagger.Extensions;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Security.Extensions;
using eDoxa.Seedwork.Security.IdentityServer.Resources;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Cashier.Api
{
    public class Startup
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

            services.AddDbContext<CashierDbContext, CashierDbContextData>(Configuration, Assembly.GetAssembly(typeof(Startup)));

            services.AddVersioning();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMvcFilters();

            services.AddSwagger(Configuration, Environment, CustomApiResources.Cashier);

            services.AddCorsPolicy();

            services.AddServiceBus(Configuration);

            services.AddAuthentication(Configuration, Environment, CustomApiResources.Cashier);

            return this.BuildModule(services);
        }

        public void Configure(IApplicationBuilder application, IApiVersionDescriptionProvider provider)
        {
            application.UseHealthChecks();

            application.UseCorsPolicy();

            application.UseCustomExceptionHandler();

            application.UseAuthentication(Environment);

            application.UseStaticFiles();

            application.UseSwagger(Environment, provider, CustomApiResources.Cashier);

            application.UseMvc();

            application.UseIntegrationEventSubscriptions();
        }

        protected virtual IServiceProvider BuildModule(IServiceCollection services)
        {
            return services.Build<ApiModule>();
        }
    }
}
