// Filename: Startup.cs
// Date Created: 2019-04-01
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
using eDoxa.Monitoring.Extensions;
using eDoxa.Notifications.Api.Extensions;
using eDoxa.Notifications.Application;
using eDoxa.Notifications.DTO.Factories;
using eDoxa.Notifications.Infrastructure;
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

namespace eDoxa.Notifications.Api
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

            services.AddHealthChecks();

            services.AddEntityFrameworkSqlServer()
                    .AddDbContext<NotificationsDbContext>(
                        options => options.UseSqlServer(
                            Configuration.GetConnectionString("SqlServer"),
                            sqlServerOptions =>
                            {
                                sqlServerOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(NotificationsDbContext)).GetName().Name);
                                sqlServerOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                            }
                        )
                    );

            services.AddDbContext<IntegrationEventLogDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("SqlServer"),
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(NotificationsDbContext)).GetName().Name);
                        sqlServerOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                    }
                )
            );

            services.AddProfiles(NotificationsMapperFactory.Instance);

            services.AddMvcWithApiBehavior();

            if (HostingEnvironment.IsDevelopment())
            {
                services.AddSwagger(
                    Configuration["Authority:External"],
                    Assembly.GetExecutingAssembly().GetName().Name,
                    config =>
                    {
                        config.ApiResourceName = Configuration["IdentityServer:ApiResources:Notifications:Name"];
                        config.ApiResourceDisplayName = Configuration["IdentityServer:ApiResources:Notifications:DisplayName"];
                        config.ApiResourceDescription = Configuration["IdentityServer:ApiResources:Notifications:Description"];
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
                            options.Audience = Configuration["IdentityServer:ApiResources:Notifications:Name"];
                            options.Authority = Configuration["Authority:Internal"];
                            options.RequireHttpsMetadata = false;
                        }
                    );

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
                        config.Id = Configuration["IdentityServer:Clients:Swagger:Notifications:ClientId"];
                        config.Name = Configuration["IdentityServer:Clients:Swagger:Notifications:ClientName"];
                    }
                );
            }

            application.UseMvcWithDefaultRoute();

            application.UseIntegrationEventSubscriptions();
        }
    }
}