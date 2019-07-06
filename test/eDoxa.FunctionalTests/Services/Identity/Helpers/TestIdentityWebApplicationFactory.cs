// Filename: IdentityWebApplicationFactory.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.IO;
using System.Reflection;

using eDoxa.Identity.Api;
using eDoxa.Identity.Infrastructure;
using eDoxa.IntegrationEvents.Infrastructure;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Security.AzureKeyVault.Extensions;
using eDoxa.Seedwork.Security.Hosting;

using JetBrains.Annotations;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.FunctionalTests.Services.Identity.Helpers
{
    internal sealed class TestIdentityWebApplicationFactory<TStartup> : WebApplicationFactory<Program>
    where TStartup : TestIdentityStartup
    {
        protected override void ConfigureWebHost([NotNull] IWebHostBuilder builder)
        {
            builder.UseEnvironment(EnvironmentNames.Testing)
                .UseContentRoot(Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(typeof(TStartup)).Location), "Services/Identity"));
        }

        [NotNull]
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder<TStartup>(Array.Empty<string>()).UseAzureKeyVault().UseSerilog();
        }

        [NotNull]
        protected override TestServer CreateServer([NotNull] IWebHostBuilder builder)
        {
            var server = base.CreateServer(builder);

            using (var scope = server.Host.Services.CreateScope())
            {
                var cashierDbContext = scope.GetService<IdentityDbContext>();

                cashierDbContext.Database.EnsureCreated();
            }

            using (var scope = server.Host.Services.CreateScope())
            {
                var cashierDbContext = scope.GetService<IntegrationEventDbContext>();

                cashierDbContext.Database.Migrate();
            }

            return server;
        }
    }
}
