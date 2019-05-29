// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Application.Filters;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMvcFilters(this IServiceCollection services, Action<FilterCollection> action = null)
        {
            var builder = services.AddMvc(options =>
            {
                action?.Invoke(options.Filters);

                options.Filters.Add<ValidationExceptionFilter>();

                options.Filters.Add<IdempotencyExceptionFilter>();
            });

            builder.AddFluentValidation(config =>
            {
                config.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });

            builder.AddControllersAsServices();

            builder.AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            builder.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
    }
}