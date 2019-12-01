// Filename: MvcBuilderExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application.DevTools.Controllers;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;

using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Application.DevTools.Extensions
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddDevTools<TDbContextSeeder, TDbContextCleaner>(this IMvcBuilder builder)
        where TDbContextSeeder : class, IDbContextSeeder
        where TDbContextCleaner : class, IDbContextCleaner
        {
            builder.Services.AddScoped<IDbContextSeeder, TDbContextSeeder>();

            builder.Services.AddScoped<IDbContextCleaner, TDbContextCleaner>();

            return builder.AddApplicationPart(typeof(DevToolsController).Assembly).AddControllersAsServices();
        }
    }
}
