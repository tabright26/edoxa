// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Application.Converters;
using eDoxa.Seedwork.Application.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMvcFilters(this IServiceCollection services)
        {
            var builder = services.AddMvc(options =>
            {
                options.Filters.Add<ValidationExceptionFilter>();
                options.Filters.Add<IdempotencyExceptionFilter>();
                options.Filters.Add<DbUpdateExceptionFilter>();
                options.Filters.Add<DbUpdateConcurrencyException>();
            });

            builder.AddControllersAsServices();

            builder.AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            builder.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
    }
}