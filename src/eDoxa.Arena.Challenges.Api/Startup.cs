﻿// Filename: Startup.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;

using eDoxa.Arena.Challenges.Api.Extensions;
using eDoxa.Arena.Challenges.Application.Modules;
using eDoxa.Arena.Challenges.Domain;
using eDoxa.Arena.Challenges.DTO.Factories;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Services.Extensions;
using eDoxa.Autofac.Extensions;
using eDoxa.AutoMapper.Extensions;
using eDoxa.Monitoring.Extensions;
using eDoxa.Security.Extensions;
using eDoxa.Security.Resources;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.ServiceBus.Extensions;
using eDoxa.Swagger.Extensions;
using eDoxa.Versioning.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Swashbuckle.AspNetCore.Swagger;

namespace eDoxa.Arena.Challenges.Api
{
    public sealed class Startup
    {
        private static readonly CustomApiResources.ChallengeApi ChallengeApi = new CustomApiResources.ChallengeApi();

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

            services.AddIntegrationEventDbContext(Configuration, Assembly.GetAssembly(typeof(ChallengesDbContext)));

            services.AddDbContext<ChallengesDbContext, ChallengesDbContextData>(Configuration);

            services.AddVersioning();

            services.AddAutoMapper(ChallengesMapperFactory.Instance);

            services.AddMvcFilters();

            services.AddSwagger(Configuration, Environment, ChallengeApi,
                options =>
                {
                    options.MapType<BestOf>(
                        () => new Schema
                        {
                            Type = "integer",
                            Format = "int32",
                            Enum = BestOf.GetAll().Cast<object>().ToList()
                        }
                    );

                    options.MapType<PayoutEntries>(
                        () => new Schema
                        {
                            Type = "integer",
                            Format = "int32",
                            Enum = PayoutEntries.GetAll().Cast<object>().ToList()
                        }
                    );

                    options.MapType<MoneyEntryFee>(
                        () => new Schema
                        {
                            Type = "string",
                            Enum = ValueObject.GetAll<MoneyEntryFee>().Cast<object>().ToList()
                        }
                    );

                    options.MapType<TokenEntryFee>(
                        () => new Schema
                        {
                            Type = "string",
                            Enum = ValueObject.GetAll<TokenEntryFee>().Cast<object>().ToList()
                        }
                    );
                }
            );

            services.AddCorsPolicy();

            services.AddServiceBus(Configuration);

            services.AddIdentityServerAuthentication(Configuration, Environment, ChallengeApi);

            services.AddUserInfoService();

            services.AddUserLoginInfoService();

            services.AddArena();

            return services.Build<ApplicationModule>();
        }

        public void Configure(IApplicationBuilder application, IApiVersionDescriptionProvider provider)
        {
            application.UseHealthChecks();

            application.UseCorsPolicy();

            application.UseCustomExceptionHandler();

            application.UseAuthentication();

            application.UseStaticFiles();

            application.UseSwagger(Environment, provider, ChallengeApi);

            application.UseMvc();

            application.UseIntegrationEventSubscriptions();
        }
    }
}