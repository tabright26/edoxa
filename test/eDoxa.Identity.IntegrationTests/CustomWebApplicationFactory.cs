// Filename: CashierWebApplicationFactory.cs
// Date Created: 2019-06-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.IO;
using System.Reflection;

using eDoxa.Identity.Api;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using EnvironmentName = eDoxa.Security.Hosting.EnvironmentName;

namespace eDoxa.Identity.IntegrationTests
{
    public sealed class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost([NotNull] IWebHostBuilder builder)
        {
            builder.UseEnvironment(EnvironmentName.Testing);

            builder.UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(CustomWebApplicationFactory)).Location));

            builder.ConfigureAppConfiguration(
                configurationBuilder =>
                {
                    configurationBuilder.AddJsonFile("appsettings.json", false).AddEnvironmentVariables();
                }
            );

            builder.ConfigureServices(services =>
                {
                    services.AddScoped<ScenarioDbContextData>();
                }
            );
        }
    }
}
