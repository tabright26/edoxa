// Filename: Startup.cs
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
using eDoxa.Identity.DTO.Factories;
using eDoxa.Identity.Infrastructure;
using eDoxa.Monitoring.Extensions;
using eDoxa.Security;
using eDoxa.Security.Extensions;
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

            services.AddDbContext<IdentityDbContext>(Configuration);

            //services.AddIdentity<User, Role>(
            //        options =>
            //        {
            //            // Password settings
            //            options.Password.RequireDigit = true;
            //            options.Password.RequiredLength = 8;
            //            options.Password.RequiredUniqueChars = 1;
            //            options.Password.RequireLowercase = true;
            //            options.Password.RequireNonAlphanumeric = true;
            //            options.Password.RequireUppercase = true;

            //            // Lockout settings
            //            options.Lockout.AllowedForNewUsers = true;
            //            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //            options.Lockout.MaxFailedAccessAttempts = 5;

            //            // User settings
            //            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
            //            options.User.RequireUniqueEmail = true;

            //            // SignIn settings
            //            if (Environment.IsProduction())
            //            {
            //                options.SignIn.RequireConfirmedEmail = true;
            //                options.SignIn.RequireConfirmedPhoneNumber = true;
            //            }
            //        }
            //    )
            //    .AddDefaultTokenProviders()
            //    .AddDefaultUI(UIFramework.Bootstrap4)
            //    .AddEntityFrameworkStores<IdentityDbContext>()
            //    .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory>();

            services.AddVersioning();

            services.AddAutoMapper(IdentityMapperFactory.Instance);

            services.AddMvcFilters();

            services.AddSwagger(Configuration, Environment);

            services.AddCorsPolicy();

            services.AddServiceBus(Configuration);

            services.AddAuthentication(Configuration, Environment, CustomScopes.IdentityApi);

            services.AddUserInfo();

            return services.Build<ApplicationModule>();
        }

        public void Configure(IApplicationBuilder application, IApiVersionDescriptionProvider provider)
        {
            application.UseHealthChecks();

            application.UseCorsPolicy();

            application.UseCustomExceptionHandler();

            application.UseAuthentication();

            application.UseStaticFiles();

            application.UseSwagger(Configuration, Environment, provider);

            application.UseMvc();

            application.UseIntegrationEventSubscriptions();
        }
    }
}