// Filename: IdentityWebApplicationFactory.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Reflection;

using eDoxa.Identity.Api;
using eDoxa.Identity.Api.Infrastructure;
using eDoxa.Seedwork.IntegrationEvents.Infrastructure;
using eDoxa.Seedwork.Security.Hosting;
using eDoxa.Seedwork.Testing.Extensions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace eDoxa.FunctionalTests.Services.Identity.Helpers
{
    internal class IdentityWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost([NotNull] IWebHostBuilder builder)
        {
            builder.UseEnvironment(EnvironmentNames.Testing);

            builder.UseContentRoot(
                Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(typeof(IdentityWebApplicationFactory)).Location), "Services/Identity")
            );

            builder.ConfigureAppConfiguration(configure => configure.AddJsonFile("appsettings.json", false).AddEnvironmentVariables());
        }

        [NotNull]
        protected override TestServer CreateServer([NotNull] IWebHostBuilder builder)
        {
            var server = base.CreateServer(builder);

            server.EnsureCreatedDbContext<IdentityDbContext>();

            server.MigrateDbContext<IntegrationEventDbContext>();

            return server;
        }
    }
}
