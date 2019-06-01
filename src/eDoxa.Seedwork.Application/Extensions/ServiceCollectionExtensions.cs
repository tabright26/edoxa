﻿// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;

using eDoxa.Security;
using eDoxa.Seedwork.Application.Filters;
using eDoxa.Seedwork.Application.Versioning;

using FluentValidation.AspNetCore;

using HealthChecks.UI.Configuration;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string GroupNameFormat = "'v'VV";

        public static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(
                options =>
                {
                    options.ApiVersionReader = new HeaderApiVersionReader(CustomHeaderNames.Version);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new DefaultApiVersion();
                    options.ReportApiVersions = true;
                }
            );

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = GroupNameFormat);
        }

        public static IServiceProvider Build<TModule>(this IServiceCollection services)
        where TModule : IModule, new()
        {
            // Create an Autofac container builder instance.
            var builder = new ContainerBuilder();

            // Register the Autofac module for a specific microservices.
            builder.RegisterModule<TModule>();

            // Populates the Autofac container builder with the set of registered service descriptors.
            builder.Populate(services);

            // Create an Autofac service provider instance.
            return new AutofacServiceProvider(builder.Build());
        }

        public static IServiceProvider Build(this IServiceCollection services)
        {
            // Create an Autofac container builder instance.
            var builder = new ContainerBuilder();

            // Populates the Autofac container builder with the set of registered service descriptors.
            builder.Populate(services);

            // Create an Autofac service provider instance.
            return new AutofacServiceProvider(builder.Build());
        }

        public static void AddMvcFilters(this IServiceCollection services, Action<FilterCollection> action = null)
        {
            var builder = services.AddMvc(options =>
            {
                action?.Invoke(options.Filters);

                options.Filters.Add<ValidationExceptionFilter>();
            });

            builder.AddFluentValidation(config =>
            {
                config.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });

            builder.AddControllersAsServices();

            builder.AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            builder.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
        
        public static void AddHealthChecksUI(this IServiceCollection services, IConfiguration configuration)
        {
            var endpoints = new List<HealthCheckSetting>();

            configuration.GetSection("HealthChecks:Endpoints").Bind(endpoints);

            services.AddHealthChecksUI(
                setupSettings: settings =>
                {
                    foreach (var endpoint in endpoints)
                    {
                        settings.AddHealthCheckEndpoint(endpoint.Name, endpoint.Uri);
                    }

                    settings.SetEvaluationTimeInSeconds(configuration.GetValue<int>("HealthChecks:EvaluationTimeInSeconds"));

                    settings.SetMinimumSecondsBetweenFailureNotifications(
                        configuration.GetValue<int>("HealthChecks:MinimumSecondsBetweenFailureNotifications")
                    );
                }
            );
        }
    }
}