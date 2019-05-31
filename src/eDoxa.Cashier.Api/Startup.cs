// Filename: Startup.cs
// Date Created: 2019-05-06
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

using eDoxa.Autofac.Extensions;
using eDoxa.Cashier.Api.Extensions;
using eDoxa.Cashier.Application.Modules;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Monitoring.Extensions;
using eDoxa.Security.Extensions;
using eDoxa.Security.Resources;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.ServiceBus.Extensions;
using eDoxa.Stripe.Extensions;
using eDoxa.Stripe.Filters;
using eDoxa.Swagger.Extensions;
using eDoxa.Versioning.Extensions;

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

            services.AddIntegrationEventDbContext(Configuration, Assembly.GetAssembly(typeof(CashierDbContext)));

            services.AddDbContext<CashierDbContext, CashierDbContextData>(Configuration);

            services.AddVersioning();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMvcFilters(filters => filters.Add<StripeExceptionFilter>());

            services.AddSwagger(Configuration, Environment, CashierApi);

            services.AddCorsPolicy();

            services.AddServiceBus(Configuration);

            services.AddIdentityServerAuthentication(Configuration, Environment, CashierApi);

            services.AddStripe();

            return services.Build<ApplicationModule>();
        }

        public void Configure(IApplicationBuilder application, IApiVersionDescriptionProvider provider)
        {
            application.UseHealthChecks();

            application.UseCorsPolicy();

            application.UseCustomExceptionHandler();

            application.UseAuthentication();

            application.UseStaticFiles();

            application.UseSwagger(Environment, provider, CashierApi);

            application.UseMvc();

            application.UseIntegrationEventSubscriptions();
        }
    }
}
