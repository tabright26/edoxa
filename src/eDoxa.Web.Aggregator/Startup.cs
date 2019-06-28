// Filename: Startup.cs
// Date Created: 2019-06-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Monitoring.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Security.Extensions;
using eDoxa.Seedwork.Security.IdentityServer.Resources;
using eDoxa.Swagger.Extensions;
using eDoxa.Web.Aggregator.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Web.Aggregator
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        private IConfiguration Configuration { get; }

        private IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks(Configuration);

            services.AddCorsPolicy();

            services.AddVersioning();

            services.AddMvcFilters();

            services.AddSwagger(
                Configuration,
                Environment,
                CustomApiResources.Aggregator,
                new[] {CustomApiResources.Identity, CustomApiResources.Cashier, CustomApiResources.ArenaChallenges}
            );

            services.AddAuthentication(Configuration, Environment, CustomApiResources.Aggregator);

            services.AddServices();
        }

        public void Configure(IApplicationBuilder application, IApiVersionDescriptionProvider provider)
        {
            application.UseHealthChecks();

            application.UseCorsPolicy();

            application.UseCustomExceptionHandler();

            application.UseAuthentication(Environment);

            application.UseStaticFiles();

            application.UseSwagger(Environment, provider, CustomApiResources.Aggregator);

            application.UseMvc();
        }
    }
}
