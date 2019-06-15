// Filename: Startup.cs
// Date Created: 2019-06-01
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
using eDoxa.Cashier.Api.Infrastructure.Data;
using eDoxa.Cashier.Infrastructure;
using eDoxa.IntegrationEvents.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Security.Extensions;
using eDoxa.Seedwork.Security.IdentityServer.Resources;
using eDoxa.Stripe.Extensions;
using eDoxa.Stripe.Filters;
using eDoxa.Swagger.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Stripe;

namespace eDoxa.Cashier.Api
{
    public sealed class Startup
    {
        private static readonly CustomApiResources.CashierApi CashierApi = new CustomApiResources.CashierApi();

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            StripeConfiguration.SetApiKey(configuration["StripeConfiguration:ApiKey"]);
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

            services.AddMvcFilters(filters => filters.Add<StripeExceptionFilter>());

            services.AddSwagger(Configuration, Environment, CashierApi);

            services.AddCorsPolicy();

            services.AddServiceBus(Configuration);

            services.AddIdentityServerAuthentication(Configuration, Environment, CashierApi);

            services.AddStripe();

            return services.Build<Modules>();
        }

        public void Configure(IApplicationBuilder application, IApiVersionDescriptionProvider provider)
        {
            application.UseHealthChecks();

            application.UseCorsPolicy();

            application.UseCustomExceptionHandler();

            application.UseAuthentication(Environment);

            application.UseStaticFiles();

            application.UseSwagger(Environment, provider, CashierApi);

            application.UseMvc();

            application.UseIntegrationEventSubscriptions();
        }
    }
}
