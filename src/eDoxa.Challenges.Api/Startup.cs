// Filename: Startup.cs
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
using eDoxa.ServiceBus;
using eDoxa.ServiceBus.Extensions;
using eDoxa.Swagger.Extensions;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Challenges.Api
{
    public sealed class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        private IHostingEnvironment HostingEnvironment { get; }

        private IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddVersioning(new ApiVersion(1, 0));

            services.AddEntityFrameworkSqlServer()
                    .AddDbContext<ChallengesDbContext>(
                        options => options.UseSqlServer(
                            Configuration.GetConnectionString("SqlServer"),
                            sqlServerOptions =>
                            {
                                sqlServerOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(ChallengesDbContext)).GetName().Name);
                                sqlServerOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                            }
                        )
                    );

            services.AddDbContext<IntegrationEventLogDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("SqlServer"),
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(ChallengesDbContext)).GetName().Name);
                        sqlServerOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                    }
                )
            );

            services.AddProfiles(ChallengesMapperFactory.Instance);

            services.AddMvcWithApiBehavior();

            if (HostingEnvironment.IsDevelopment())
            {
                services.AddSwagger(
                    Configuration["Authority:External"],
                    Assembly.GetExecutingAssembly().GetName().Name,
                    config =>
                    {
                        config.ApiResourceName = Configuration["IdentityServer:ApiResources:Challenges:Name"];
                        config.ApiResourceDisplayName = Configuration["IdentityServer:ApiResources:Challenges:DisplayName"];
                        config.ApiResourceDescription = Configuration["IdentityServer:ApiResources:Challenges:Description"];
                    }
                );
            }

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
                            options.Audience = Configuration["IdentityServer:ApiResources:Challenges:Name"];
                            options.Authority = Configuration["Authority:Internal"];
                            options.RequireHttpsMetadata = false;
                        }
                    );

            services.AddHealthChecks(Configuration);

            return services.Build<ApplicationModule>();
        }

        public void Configure(IApplicationBuilder application, IApiVersionDescriptionProvider provider)
        {
            application.UseCorsPolicy();

            application.UseHealthChecks();

            application.UseAuthentication();

            application.UseStaticFiles();

            if (HostingEnvironment.IsDevelopment())
            {
                application.UseSwaggerWithRedirects(
                    provider,
                    config =>
                    {
                        config.Id = Configuration["IdentityServer:Clients:Swagger:Challenges:ClientId"];
                        config.Name = Configuration["IdentityServer:Clients:Swagger:Challenges:ClientName"];
                    }
                );
            }

            application.UseMvcWithDefaultRoute();

            application.UseIntegrationEventSubscriptions();
        }
    }
}