// Filename: Startup.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;

using Autofac;

using AutoMapper;

using eDoxa.Cashier.Api.Extensions;
using eDoxa.Cashier.Api.Infrastructure;
using eDoxa.Cashier.Api.Infrastructure.Data;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Application.DomainEvents;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.Swagger.Extensions;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Security.Constants;
using eDoxa.Seedwork.Security.Extensions;
using eDoxa.Seedwork.ServiceBus;
using eDoxa.Seedwork.ServiceBus.Extensions;
using eDoxa.Seedwork.ServiceBus.Modules;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using static eDoxa.Seedwork.Security.IdentityServer.Resources.CustomApiResources;

namespace eDoxa.Cashier.Api
{
    public sealed class Startup
    {
        private static readonly string XmlCommentsFilePath = Path.Combine(
            AppContext.BaseDirectory,
            $"{typeof(Startup).GetTypeInfo().Assembly.GetName().Name}.xml"
        );

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            AppSettings = configuration.GetAppSettings<CashierAppSettings>(CashierApi);
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        private CashierAppSettings AppSettings { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppSettings<CashierAppSettings>(Configuration);

            services.AddHealthChecks(AppSettings);

            services.AddCorsPolicy();

            services.AddEntityFrameworkSqlServer();

            services.AddIntegrationEventDbContext(AppSettings.ConnectionStrings.SqlServer, Assembly.GetAssembly(typeof(Startup)));

            services.AddDbContext<CashierDbContext, CashierDbContextData>(AppSettings.ConnectionStrings.SqlServer, Assembly.GetAssembly(typeof(Startup)));

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VV");

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddControllersAsServices()
                .AddFluentValidation(config => config.RunDefaultMvcValidationAfterFluentValidationExecutes = false);

            services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.ApiVersionReader = new HeaderApiVersionReader(CustomHeaderNames.Version);
                }
            );

            services.AddAutoMapper(Assembly.GetAssembly(typeof(Startup)), Assembly.GetAssembly(typeof(CashierDbContext)));

            if (AppSettings.SwaggerEnabled)
            {
                services.AddSwaggerGen(
                    options =>
                    {
                        var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                        foreach (var description in provider.ApiVersionDescriptions)
                        {
                            options.SwaggerDoc(description.GroupName, description.CreateInfoForApiVersion(AppSettings));
                        }

                        options.IncludeXmlComments(XmlCommentsFilePath);

                        options.AddSecurityDefinition(AppSettings);

                        options.AddFilters();
                    }
                );
            }

            services.AddAuthentication(HostingEnvironment, AppSettings);

            services.AddServiceBus(AppSettings);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<DomainEventModule>();

            builder.RegisterModule<RequestModule>();

            builder.RegisterModule<IntegrationEventModule<CashierDbContext>>();

            builder.RegisterModule<CashierApiModule>();
        }

        public void Configure(IApplicationBuilder application, IApiVersionDescriptionProvider provider)
        {
            application.UseHealthChecks();

            application.UseCorsPolicy();

            application.UseCustomExceptionHandler();

            application.UseAuthentication(HostingEnvironment);

            if (AppSettings.SwaggerEnabled)
            {
                application.UseSwagger();

                application.UseSwaggerUI(
                    options =>
                    {
                        foreach (var description in provider.ApiVersionDescriptions)
                        {
                            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                        }

                        options.RoutePrefix = string.Empty;

                        options.OAuthClientId(AppSettings.ApiResource.SwaggerClientId());

                        options.OAuthAppName(AppSettings.ApiResource.SwaggerClientName());

                        options.DefaultModelExpandDepth(0);

                        options.DefaultModelsExpandDepth(-1);
                    }
                );
            }

            application.UseMvc();

            application.UseIntegrationEventSubscriptions();
        }
    }
}
