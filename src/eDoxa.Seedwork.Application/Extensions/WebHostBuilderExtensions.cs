// Filename: WebHostBuilderExtensions.cs
// Date Created: 2019-12-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseCustomAutofac(this IWebHostBuilder builder)
        {
            return builder.ConfigureServices(services => services.AddAutofac());
        }
    }
}
