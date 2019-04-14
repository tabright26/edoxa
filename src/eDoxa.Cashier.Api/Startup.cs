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
using eDoxa.Cashier.Api.Extensions;
using eDoxa.Cashier.Application;
using eDoxa.Cashier.DTO.Factories;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Monitoring.Extensions;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.ServiceBus;
using eDoxa.ServiceBus.Extensions;
using eDoxa.Stripe.Extensions;
using eDoxa.Swagger.Extensions;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Cashier.Api
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
            services.AddHealthChecks(Configuration);

            services.AddVersioning(new ApiVersion(1, 0));

            services.AddEntityFrameworkSqlServer()
                    .AddDbContext<CashierDbContext>(
                        options => options.UseSqlServer(
                            Configuration.GetConnectionString("Sql"),
                            sqlServerOptions =>
                            {
                                sqlServerOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(CashierDbContext)).GetName().Name);
                                sqlServerOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                            }
                        )
                    );

            services.AddDbContext<IntegrationEventLogDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("Sql"),
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(CashierDbContext)).GetName().Name);
                        sqlServerOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                    }
                )
            );

            services.AddProfiles(CashierMapperFactory.Instance);

            services.AddMvcWithApiBehavior();

            if (HostingEnvironment.IsDevelopment())
            {
                services.AddSwagger(
                    Configuration["Authority"],
                    Assembly.GetExecutingAssembly().GetName().Name,
                    config =>
                    {
                        config.ApiResourceName = Configuration["IdentityServer:ApiResource:Name"];
                        config.ApiResourceDisplayName = Configuration["IdentityServer:ApiResource:DisplayName"];
                        config.ApiResourceDescription = Configuration["IdentityServer:ApiResource:Description"];
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
                            options.Audience = Configuration["IdentityServer:ApiResource:Name"];
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

            if (HostingEnvironment.IsDevelopment())
            {
                application.UseSwaggerWithRedirects(
                    provider,
                    config =>
                    {
                        config.Id = Configuration["IdentityServer:Client:Swagger:ClientId"];
                        config.Name = Configuration["IdentityServer:Client:Swagger:ClientName"];
                    }
                );
            }

            application.UseMvcWithDefaultRoute();

            application.UseIntegrationEventSubscriptions();

            application.UseStripe(Configuration);
        }
    }
}