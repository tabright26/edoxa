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
using Autofac.Extensions.DependencyInjection;

using AutoMapper;

using eDoxa.Cashier.Api.Application.Factories;
using eDoxa.Cashier.Api.Application.Services;
using eDoxa.Cashier.Api.Application.Strategies;
using eDoxa.Cashier.Api.Extensions;
using eDoxa.Cashier.Api.Infrastructure;
using eDoxa.Cashier.Api.Infrastructure.Data;
using eDoxa.Cashier.Api.Infrastructure.Queries;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.Domain.Strategies;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Repositories;
using eDoxa.Commands;
using eDoxa.IntegrationEvents;
using eDoxa.IntegrationEvents.Extensions;
using eDoxa.Seedwork.Application.DomainEvents;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.Swagger;
using eDoxa.Seedwork.Application.Swagger.Extensions;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Security.Constants;
using eDoxa.Seedwork.Security.Extensions;

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

        public static Action<ContainerBuilder> ConfigureContainerBuilder = builder =>
        {
            // Required for testing.
        };

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            AppSettings = configuration.TryGetAppSettings(IdentityApi);
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        private AppSettings AppSettings { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks(Configuration);

            services.AddCorsPolicy();

            services.AddEntityFrameworkSqlServer();

            services.AddIntegrationEventDbContext(Configuration, Assembly.GetAssembly(typeof(Startup)));

            services.AddDbContext<CashierDbContext, CashierDbContextData>(Configuration, Assembly.GetAssembly(typeof(Startup)));

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

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            if (AppSettings.IsValid())
            {
                if (AppSettings.Swagger.Enabled)
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
            }

            services.AddAuthentication(Configuration, HostingEnvironment, CashierApi);

            // Repositories
            services.AddScoped<IChallengeRepository, ChallengeRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            // Queries
            services.AddScoped<IChallengeQuery, ChallengeQuery>();
            services.AddScoped<IAccountQuery, AccountQuery>();
            services.AddScoped<ITransactionQuery, TransactionQuery>();

            // Services
            services.AddScoped<IAccountService, AccountService>();

            // Strategies
            services.AddTransient<IPayoutStrategy, PayoutStrategy>();

            // Factories
            services.AddSingleton<IPayoutFactory, PayoutFactory>();

            services.AddServiceBus(Configuration);

            return CreateContainer(services);
        }

        public void Configure(IApplicationBuilder application, IApiVersionDescriptionProvider provider)
        {
            application.UseHealthChecks();

            application.UseCorsPolicy();

            application.UseCustomExceptionHandler();

            application.UseAuthentication(HostingEnvironment);

            if (AppSettings.IsValid())
            {
                if (AppSettings.Swagger.Enabled)
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
            }

            application.UseMvc();

            application.UseIntegrationEventSubscriptions();
        }

        private static IServiceProvider CreateContainer(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<DomainEventModule>();

            builder.RegisterModule<CommandModule>();

            builder.RegisterModule<IntegrationEventModule<CashierDbContext>>();

            builder.RegisterModule<CashierApiModule>();

            builder.Populate(services);

            ConfigureContainerBuilder(builder);

            return new AutofacServiceProvider(builder.Build());
        }
    }
}
